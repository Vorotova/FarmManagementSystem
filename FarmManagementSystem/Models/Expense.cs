using System;

namespace FarmManagementSystem.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string ExpenseType { get; set; }
        public int WorkId { get; set; }

        // Навігаційна властивість
        public Work Work { get; set; }

        public Expense()
        {
        }

        public Expense(int id, int amount, DateTime date, string expenseType, int workId)
        {
            Id = id;
            Amount = amount;
            Date = date;
            ExpenseType = expenseType;
            WorkId = workId;
        }

        public string GetExpenseReport()
        {
            return $"Витрата: {ExpenseType}, " +
                   $"Сума: {Amount} грн, " +
                   $"Дата: {Date.ToShortDateString()}, " +
                   $"Пов'язана робота: {Work?.WorkType?.Name ?? "Невідома робота"} на полі {Work?.Field?.Name ?? "Невідоме поле"}";
        }

        public override string ToString()
        {
            return GetExpenseReport();
        }
    }
} 