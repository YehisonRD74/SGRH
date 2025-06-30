using System;
using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Employee : User
    {
        public DateTime HireDate { get; private set; }

        public Employee(string firstName, string lastName, string email, string phoneNumber, string address, string passwordHash, DateTime hireDate)
            : base(firstName, lastName, email, phoneNumber, address, passwordHash)
        {
            HireDate = hireDate;
        }

        protected Employee() : base() { }

        public override string Rol => "Employee";
    }
}