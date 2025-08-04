using SGRH._Domain.Base;

namespace SGRH._Domain.Entites
{
    public class Room : BaseEntity
    {
        public string Status { get; set; }
        public string NumeroHabitacion { get; set; }
        public String Type { get; set; }
        public decimal Price { get; set; }
        
        public int RoomCategoryId { get; set; }
        public int FloorId { get; set; }


        public Room(String numeroHabitacion, int RoomCategoryId, string status, string type)
        {
            NumeroHabitacion = numeroHabitacion;
            Status = status;
            RoomCategoryId = RoomCategoryId;
            Type = type;
        }


        public Room() { }


        
    }
}