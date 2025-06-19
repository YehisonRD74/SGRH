namespace SGRH._Domain.Base;

public abstract class User: AuditEntity
{
    public int Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public String address { get; set; }
    public String Password{ get; set; }
    

    public abstract String Rol { get; }
    
    public User(int Id, string FirstName, string LastName, string Email, string PhoneNumber, string address, string Password) 
        : base(Id, FirstName, LastName, Email, PhoneNumber, address, Password)
    {
        this.Id = Id;   
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.address = address;
        this.Password = Password;
        
        
        
    }
    
}