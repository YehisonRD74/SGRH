namespace SGRH._Domain.Entities
{
    public class Floor
    {
        public int Id { get; private set; }
        public int NumeroPiso { get; private set; }
        
        public ICollection<Room> Room { get; private set; }
        public Floor(int numeroPiso)
        {
            NumeroPiso = numeroPiso;
        }
    }
}
