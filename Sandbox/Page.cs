using System;
using MessagePack;

namespace Sandbox
{
    [MessagePackObject]
    public class Page
    {
        public const int Size = 1024;
        [Key(0)]
        public byte[] Records = new byte[Size];
        [Key(1)]
        public int[] Offsets = new int[Size];
    }
}
