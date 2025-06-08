using System;

namespace FarmManagementSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int HarvestId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } = "Active";
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        
        public Client Client { get; set; }
        public Harvest Harvest { get; set; }
        
        public Sale()
        {
        }
        
        public int CalculateTotalAmount()
        {
            return Quantity * UnitPrice;
        }
        
        public bool IsContract()
        {
            return ContractDate.HasValue;
        }
        
        public string GetSaleType()
        {
            return IsContract() ? "Контракт" : "Продаж";
        }
        
        public string GetProductType()
        {
            return Harvest?.Culture?.Name ?? "Невідомо";
        }
        
        public string GetSaleSummary()
        {
            return $"{GetSaleType()}: {GetProductType()} для {Client?.CompanyName ?? "Невідомий клієнт"}, " +
                   $"Кількість: {Quantity} кг, " +
                   $"Ціна за кг: {UnitPrice} грн, " +
                   $"Загальна сума: {CalculateTotalAmount()} грн, " +
                   $"Статус: {Status}";
        }
        
        public override string ToString()
        {
            return GetSaleSummary();
        }
    }
} 