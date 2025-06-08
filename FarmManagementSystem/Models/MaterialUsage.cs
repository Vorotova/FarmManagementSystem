using System;

namespace FarmManagementSystem.Models
{
    public class MaterialUsage
    {
        public int Id { get; set; }
        public int MaterialTypeId { get; set; }
        public int Quantity { get; set; }
        public int WorkId { get; set; }

        // Навігаційні властивості
        public MaterialType MaterialType { get; set; }
        public Work Work { get; set; }

        public MaterialUsage()
        {
        }

        public MaterialUsage(int id, int materialTypeId, int quantity, int workId)
        {
            Id = id;
            MaterialTypeId = materialTypeId;
            Quantity = quantity;
            WorkId = workId;
        }

        public string GetUsageDetails()
        {
            return $"Використання матеріалу: {MaterialType?.Name ?? "Невідомий матеріал"}, " +
                   $"Кількість: {Quantity} {MaterialType?.Unit ?? "од."}, " +
                   $"Робота: {Work?.WorkType?.Name ?? "Невідома робота"} на полі {Work?.Field?.Name ?? "Невідоме поле"}";
        }

        public override string ToString()
        {
            return GetUsageDetails();
        }
    }
} 