using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Employee : Person
    {
        public int Id { get; private set; }
        public string Position { get; private set; }
        public DateTime HireDate { get; private set; }

        public Employee(
            int id,
            string name,
            string lastName,
            string email,
            string phoneNumber,
            int gender,
            DateTime dateOfBirth,
            string nationality,
            string position,
            DateTime hireDate
        ) : base(name, lastName, email, phoneNumber, gender, dateOfBirth, nationality)
        {
            Id = id;
            Position = position;
            HireDate = hireDate;
        }
    }
}
