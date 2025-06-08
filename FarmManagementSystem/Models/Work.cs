using System;

namespace FarmManagementSystem.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int WorkTypeId { get; set; }
        public int FieldId { get; set; }
        public int? TechniqueId { get; set; }
        public int? EmployeeId { get; set; }

        // Навігаційні властивості
        public WorkType WorkType { get; set; }
        public Field Field { get; set; }
        public Technique Technique { get; set; }
        public Employee Employee { get; set; }

        public Work()
        {
        }

        public Work(int id, int duration, DateTime date, int workTypeId, int fieldId, int? techniqueId, int? employeeId)
        {
            Id = id;
            Duration = duration;
            Date = date;
            WorkTypeId = workTypeId;
            FieldId = fieldId;
            TechniqueId = techniqueId;
            EmployeeId = employeeId;
        }

        public void AssignEmployee(Employee employee)
        {
            if (employee != null)
            {
                EmployeeId = employee.Id;
                Employee = employee;
            }
            else
            {
                EmployeeId = null;
                Employee = null;
            }
        }

        public void AssignTechnique(Technique technique)
        {
            if (technique != null)
            {
                TechniqueId = technique.Id;
                Technique = technique;
            }
            else
            {
                TechniqueId = null;
                Technique = null;
            }
        }

        public double CalculateWorkCost()
        {
            double cost = 0;
            if (Technique != null)
            {
                cost += Technique.CalculateUsageCost(Duration);
            }
            return cost;
        }

        public override string ToString()
        {
            return $"Робота: {WorkType?.Name ?? "Невідомий тип роботи"}, " +
                   $"На полі: {Field?.Name ?? "Невідоме поле"}, " +
                   $"Дата: {Date.ToShortDateString()}, " +
                   $"Тривалість: {Duration} годин, " +
                   $"Працівник: {Employee?.FullName ?? "Не призначено"}, " +
                   $"Техніка: {Technique?.Name ?? "Не призначено"}";
        }
    }
} 