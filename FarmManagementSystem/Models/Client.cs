using System;
using System.Collections.Generic;

namespace FarmManagementSystem.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public List<Sale> Sales { get; set; } = new List<Sale>();
        
        public Client()
        {
        }
        
        public int GetContractsCount()
        {
            return Sales?.Count ?? 0;
        }
        
        public int GetTotalSalesAmount()
        {
            int total = 0;
            if (Sales != null)
            {
                foreach (var sale in Sales)
                {
                    total += sale.CalculateTotalAmount();
                }
            }
            return total;
        }
        
        public string GetClientInfo()
        {
            return $"Клієнт: {CompanyName}\n" +
                   $"Контактна особа: {ContactPerson}\n" +
                   $"Телефон: {Phone}\n" +
                   $"Email: {Email ?? "Не вказано"}\n" +
                   $"Кількість контрактів: {GetContractsCount()}\n" +
                   $"Загальна сума продажів: {GetTotalSalesAmount()} грн";
        }
        
        public override string ToString()
        {
            return $"{CompanyName} ({ContactPerson}) - Контрактів: {GetContractsCount()}";
        }
    }
} 