
namespace SGRH.Web.Models.Room
{
    public class CreateRoomModel
    {
        public string NumeroHabitacion { get; set; }
        public string Type { get; set; }
        public int FloorId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int RoomCategoryId { get; set; }
        public DateTime UpdatedAt { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public string CreatedBy { get;  set; }
        public string UpdatedBy { get;  set; }
        public bool IsDeleted { get;  set; }
        public bool IsDeletedBy { get;  set;}

    }
}
