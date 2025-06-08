using System;

namespace FarmManagementSystem.Models
{
    public class Planting
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public int CultureId { get; set; }
        public DateTime SowingDate { get; set; }

        // Навігаційні властивості (опціонально для зручності)
        public Field Field { get; set; }
        public Culture Culture { get; set; }

        public Planting()
        {
        }

        public Planting(int id, int fieldId, int cultureId, DateTime sowingDate)
        {
            Id = id;
            FieldId = fieldId;
            CultureId = cultureId;
            SowingDate = sowingDate;
        }

        public void RegisterPlanting()
        {
            // Тут можна додати логіку реєстрації посадки
            // Наприклад, перевірка дати, валідація даних тощо
        }

        public string GetPlantingInfo()
        {
            return $"Посадка: {Culture?.Name ?? "Невідома культура"} на полі {Field?.Name ?? "Невідоме поле"}, " +
                   $"Дата посадки: {SowingDate.ToShortDateString()}";
        }

        public override string ToString()
        {
            return GetPlantingInfo();
        }
    }
} 