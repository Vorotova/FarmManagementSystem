using System;

namespace FarmManagementSystem.Models
{
    public class Culture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Seasonality { get; set; }
        public int AverageYield { get; set; }

        public Culture()
        {
        }

        public Culture(int id, string name, string seasonality, int averageYield)
        {
            Id = id;
            Name = name;
            Seasonality = seasonality;
            AverageYield = averageYield;
        }

        public string GetInfo()
        {
            return $"Культура: {Name}, Сезонність: {Seasonality}, Середній урожай: {AverageYield}";
        }

        public void UpdateAverageYield(int newValue)
        {
            if (newValue >= 0)
            {
                AverageYield = newValue;
            }
            else
            {
                throw new ArgumentException("Середній урожай не може бути від'ємним");
            }
        }
    }
} 