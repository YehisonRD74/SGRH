using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Administrator : User
    {
        public Administrator(string firstName, string lastName, string email, string phoneNumber, string address, string passwordHash)
            : base(firstName, lastName, email, phoneNumber, address, passwordHash)
        {
        }

        public override string Rol => "Administrador";

        protected Administrator() : base() { }
    }
}