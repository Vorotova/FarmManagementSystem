using System;
using System.Collections.Generic;

namespace FarmManagementSystem.Models
{
    public class Technique
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double UsageCost { get; set; }
        public string Condition { get; set; }

        // Навігаційна властивість для зв'язку з роботами
        public List<Work> Works { get; set; } = new List<Work>();

        public Technique()
        {
        }

        public Technique(int id, string name, string type, double usageCost, string condition)
        {
            Id = id;
            Name = name;
            Type = type;
            UsageCost = usageCost;
            Condition = condition;
        }

        public double CalculateUsageCost(int hours)
        {
            if (hours < 0)
                throw new ArgumentException("Кількість годин не може бути від'ємною");
            
            return UsageCost * hours;
        }

        public string CheckCondition()
        {
            return $"Стан техніки {Name}: {Condition}";
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - {Condition}";
        }
    }
} 