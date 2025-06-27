using SGRH._Domain.Base;

public class Administrator : User
{
    public Administrator(int Id, string FirstName, string LastName, string Email, string PhoneNumber, string address, string Password)
        : base(Id, FirstName, LastName, Email, PhoneNumber, address, Password)
    {
    }

    public override string Rol => "Administrador";
    
}
