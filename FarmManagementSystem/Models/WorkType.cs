using System;

namespace FarmManagementSystem.Models
{
    public class WorkType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public WorkType() { }
        public WorkType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
} 