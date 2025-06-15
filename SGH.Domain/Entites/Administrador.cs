using SGRH._Domain.Base;

public class Administrator : Person
{
    public int Id { get; private set; }
    public string Role { get; private set; } = "Administrator";
    public DateTime HireDate { get; private set; }

    public Administrator(
        string name,
        string lastName,
        string email,
        string phoneNumber,
        int gender,
        DateTime dateOfBirth,
        string nationality,
        string createdBy
    ) : base(name, lastName, email, phoneNumber, gender, dateOfBirth, nationality)
    {

    }
}
