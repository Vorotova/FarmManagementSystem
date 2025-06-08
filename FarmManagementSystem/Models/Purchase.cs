using System;

namespace FarmManagementSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } = "Active";
        public string Notes { get; set; }

        // Навігаційні властивості
        public MaterialType Material { get; set; }
        public Supplier Supplier { get; set; }

        public Purchase()
        {
        }

        public Purchase(int id, int materialId, int supplierId, DateTime date, int quantity, int unitPrice, 
                       DateTime? contractDate = null, DateTime? deliveryDate = null, string status = "Active", string notes = "")
        {
            Id = id;
            MaterialId = materialId;
            SupplierId = supplierId;
            Date = date;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ContractDate = contractDate;
            DeliveryDate = deliveryDate;
            Status = status;
            Notes = notes;
        }

        public int CalculateTotalPrice()
        {
            return Quantity * UnitPrice;
        }

        public bool IsContract()
        {
            return ContractDate.HasValue;
        }

        public override string ToString()
        {
            string type = IsContract() ? "Контракт" : "Закупка";
            return $"{type}: {Material?.Name ?? "Невідомий матеріал"} від {Supplier?.Name ?? "Невідомий постачальник"}, " +
                   $"Дата: {Date.ToShortDateString()}, " +
                   $"Кількість: {Quantity} {Material?.Unit ?? "од."}, " +
                   $"Ціна за одиницю: {UnitPrice} грн, " +
                   $"Загальна вартість: {CalculateTotalPrice()} грн, " +
                   $"Статус: {Status}";
        }
    }
} 