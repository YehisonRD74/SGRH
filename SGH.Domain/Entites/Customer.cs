using SGRH._Domain.Base;

namespace YourProject.Domain.Entities
{
    public class Customer : User
    {
        public Customer(string firstName, string lastName, string email, string phoneNumber, string address, string passwordHash)
            : base(firstName, lastName, email, phoneNumber, address, passwordHash)
        {
        }

        public override string Rol => "Customer";

        protected Customer() : base() { }
    }
}