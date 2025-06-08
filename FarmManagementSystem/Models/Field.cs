using System;

namespace FarmManagementSystem.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public string SoilType { get; set; }

        public Field()
        {
        }

        public Field(int id, string name, int area, string soilType)
        {
            Id = id;
            Name = name;
            Area = area;
            SoilType = soilType;
        }

        public int CalculateTotalArea()
        {
            return Area;
        }

        public string GetSoilType()
        {
            return SoilType;
        }

        public override string ToString()
        {
            return $"Поле: {Name}, Площа: {Area} га, Тип ґрунту: {SoilType}";
        }
    }
} 