using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Room : BaseEntity
    {
        public string Estado { get; set; }
        public int NumeroHabitacion { get; set; }
        public String Type { get; set; }
        public decimal Price { get; set; }
        
        public int RoomCategoryId { get; set; }

        public Room(int numeroHabitacion, string estado, int roomCategoryId)
        {
            NumeroHabitacion = numeroHabitacion;
            Estado = estado;
            RoomCategoryId = roomCategoryId;
        }

        protected Room() : base() { }
    }
}