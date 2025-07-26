using System;
using SGRH._Domain.Base;

namespace SGRH._Domain.Base
{
    public abstract class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PasswordHash { get; set; }

        public abstract string Rol { get; }

        protected User(string firstName, string lastName, string email, string phoneNumber, string address, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PasswordHash = passwordHash;
        }

        protected User() { }
    }
}