namespace SGRH.Application.DTO.dbo
{
    public record CreateRoomDTO
    {
        public int NumeroHabitacion { get; init; }
        public string Estado { get; init; }
        public int RoomCategoryId { get; init; }
        public int FloorId { get; }
        public String Type { get; set; }
        public decimal Price { get; set; }
      
    }
}