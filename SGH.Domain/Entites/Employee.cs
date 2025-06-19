using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Employee : User
    {   
        public DateTime HireDate { get; private set; }
        public Employee(int Id, string FirstName, string LastName, string Email, string PhoneNumber, string address, string Password, DateTime hireDate) : base(Id, FirstName, LastName, Email, PhoneNumber, address, Password)
        {
            HireDate = hireDate;
        }
        
        public override string Rol =>"Employee";
    }
}
