using System;
using System.Collections.Generic;

namespace FarmManagementSystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string ProductType { get; set; }

        // Навігаційна властивість для зв'язку з закупками
        public List<Purchase> Purchases { get; set; } = new List<Purchase>();

        public Supplier()
        {
        }

        public Supplier(int id, string name, string contactPerson, string phone, string productType)
        {
            Id = id;
            Name = name;
            ContactPerson = contactPerson;
            Phone = phone;
            ProductType = productType;
        }

        public string GetSupplierInfo()
        {
            return $"Постачальник: {Name}\n" +
                   $"Контактна особа: {ContactPerson}\n" +
                   $"Телефон: {Phone}\n" +
                   $"Тип продукції: {ProductType}";
        }

        public override string ToString()
        {
            return $"{Name} - {ProductType}";
        }
    }
} 