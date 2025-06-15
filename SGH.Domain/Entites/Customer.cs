using SGRH._Domain.Base;

namespace YourProject.Domain.Entities
{
    public class Customer : Person
    {
        public int Id { get; private set; }
        public string Address { get; private set; }
        public string Password { get; private set; }

        public Customer(int id, string name, string lastName, string email, string phoneNumber, int gender, DateTime dateOfBirth, string nationality, string address, string password)
            : base(name, lastName, email, phoneNumber, gender, dateOfBirth, nationality)
        {
            Id = id;
            Address = address;
            Password = password;
        }
    }
}