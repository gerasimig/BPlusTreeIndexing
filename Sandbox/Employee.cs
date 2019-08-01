using System;
using MessagePack;

namespace Sandbox
{
    [MessagePackObject]
    public class Employee
    {
        [Key(0)]
        public int Id { get; }

        [Key(1)]
        public string FirstName { get; }

        public Employee(int id, string firstName)
        {
            Id = id;
            FirstName = firstName;
        }
    }
}
