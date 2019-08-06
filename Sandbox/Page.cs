using System;
using System.Collections.Generic;
using System.ComponentModel;
using MessagePack;

namespace Sandbox
{
    [MessagePackObject]
    public class Page
    {
        public const int Size = 8192;
        public const int OffsetSize = 512;

        private readonly int MinRecordSize = Size / OffsetSize / RecordId.RecordIdSize;

        public byte[] Bytes = new byte[Size];
        private RecordId[] Locations;

        public Page(byte[] pageBytes)
        {
            if(pageBytes.Length != Size)
            {
                throw new ArgumentException("Page is not valid");
            }

            Array.Copy(pageBytes, Bytes, Size);
            
        }

        public void AddRecord<T>(T record)
        {
            byte[] bytes = MessagePackSerializer.Serialize(record);

            if (bytes.Length > Size)
            {
                throw new ArgumentException("Record is too large");
            }
        }
    }

    // Represents record address in page (RecordId)
    public class RecordId
    {
        public const int RecordIdSize = 8;
        public const int IntSize = 4;

        public int PageId { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }

        public static List<RecordId> DeseriaRecordIds(byte[] byteLocations)
        {
            if(byteLocations.Length % RecordIdSize != 0)
            {
                throw new ArgumentException("Record Locations are invalid"); 
            }

            var recordLocations = new List<RecordId>();
            var intArr = new byte[4];

            for (var i = 0; i < byteLocations.Length; i += RecordIdSize)
            {
                Array.Copy(byteLocations, i, intArr, 0, IntSize);
                var offset = BitConverter.ToInt32(intArr);

                Array.Copy(byteLocations, i + IntSize, intArr, 0, IntSize);
                var length = BitConverter.ToInt32(intArr);

                if (offset == 0 && length == 0) break;
                recordLocations.Add(new RecordId { Offset = offset, Length = length });
            }

            return recordLocations;
        }
    }
}
