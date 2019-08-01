using System;
using System.Collections.Generic;
using MessagePack;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new List<Employee>();

            var emp = new Employee(1, "E");
            var page = new Page();
            byte[] bytes = MessagePackSerializer.Serialize(page);
            Employee emp2 = MessagePackSerializer.Deserialize<Employee>(bytes);
            Console.WriteLine(emp2.Id + emp2.FirstName);
        }
    }
}
