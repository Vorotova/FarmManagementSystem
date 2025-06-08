using System;
using System.Collections.Generic;

namespace FarmManagementSystem.Models
{
    public class MaterialType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }

        // Навігаційні властивості
        public List<Purchase> Purchases { get; set; } = new List<Purchase>();
        public List<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();

        public MaterialType()
        {
        }

        public MaterialType(int id, string name, string type, string unit)
        {
            Id = id;
            Name = name;
            Type = type;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - {Unit}";
        }
    }
} 