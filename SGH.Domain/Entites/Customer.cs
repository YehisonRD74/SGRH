using SGRH._Domain.Base;

namespace YourProject.Domain.Entities
{
    public class Customer : User

    {
        public Customer(int Id, string FirstName, string LastName, string Email, string PhoneNumber, string address, string Password) : base(Id, FirstName, LastName, Email, PhoneNumber, address, Password)
        {
        }

        public override string Rol => "Customer";
    }
}

