
namespace SGRH._Domain.Base
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{Name} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; private set; }
        public string Nationality { get; private set; }
        protected Person(string Name, string LastName, string Email, string PhoneNumber, int Gender, DateTime DateOfBirth, String Nationality)
        {
            //Bueno aqui hice unan codicion para que Name y LastName no sea null , osea es obligatorio asignarle nombre y apellido
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name can not be Empty.", nameof(Name));

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Last Can not be Empty.", nameof(LastName));
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email not be Empty.", nameof(Email));

            this.Name = Name;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;

        }
        protected Person() { }
    }

}
