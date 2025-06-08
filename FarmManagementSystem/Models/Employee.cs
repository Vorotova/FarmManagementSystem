using System;
using System.Collections.Generic;

namespace FarmManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }

        // Навігаційна властивість для зв'язку з роботами
        public List<Work> Works { get; set; } = new List<Work>();

        public Employee()
        {
        }

        public Employee(int id, string fullName, string phone, string position)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Position = position;
        }

        public void AssignWork(Work work)
        {
            if (work != null)
            {
                work.EmployeeId = this.Id;
                Works.Add(work);
            }
        }

        public string Contact()
        {
            return $"Контактна інформація: {FullName}, Телефон: {Phone}, Посада: {Position}";
        }

        public override string ToString()
        {
            return $"{FullName} - {Position}";
        }
    }
} 