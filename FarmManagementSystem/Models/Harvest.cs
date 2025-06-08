using System;

namespace FarmManagementSystem.Models
{
    public class Harvest
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public int CultureId { get; set; }
        public DateTime HarvestDate { get; set; }
        public int Volume { get; set; }
        public int PricePerKg { get; set; }
        
        public int AvailableQuantity { get; set; }

        public Field Field { get; set; }
        public Culture Culture { get; set; }

        public Harvest()
        {
        }

        public Harvest(int id, int fieldId, int cultureId, DateTime harvestDate, int volume, int pricePerKg)
        {
            Id = id;
            FieldId = fieldId;
            CultureId = cultureId;
            HarvestDate = harvestDate;
            Volume = volume;
            PricePerKg = pricePerKg;
        }

        public int CalculateTotalValue()
        {
            return Volume * PricePerKg;
        }

        public string GetHarvestSummary()
        {
            return $"Урожай: {Culture?.Name ?? "Невідома культура"} з поля {Field?.Name ?? "Невідоме поле"}, " +
                   $"Дата збору: {HarvestDate.ToShortDateString()}, " +
                   $"Об'єм: {Volume} кг, " +
                   $"Ціна за кг: {PricePerKg} грн, " +
                   $"Загальна вартість: {CalculateTotalValue()} грн";
        }
        
        public string GetAvailableHarvestSummary()
        {
            return $"{Culture?.Name ?? "Невідома культура"} - {HarvestDate.ToShortDateString()} - Доступно: {AvailableQuantity} кг (ціна: {PricePerKg} грн/кг)";
        }

        public override string ToString()
        {
            return GetHarvestSummary();
        }
    }
} 