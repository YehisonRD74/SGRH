using System.Text.Json.Serialization;

namespace SGRH.Web.Models
{
    public class RoomModels
    {
        [JsonPropertyName("roomId")]
        public int Id { get; set; }

        public string NumeroHabitacion { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int FloorId { get; set; }
        public string Status { get; set; }

        public int RoomCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class GetAllRoomResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<RoomModels> Data { get; set; }
    }
 
    public class GetRoomResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public RoomModels Data { get; set; }
    }

}
