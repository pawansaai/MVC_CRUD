﻿namespace WebApplication1.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Department { get; set; }
        public long Salary { get; set; }

    }
}
