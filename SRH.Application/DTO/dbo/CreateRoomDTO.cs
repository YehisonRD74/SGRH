namespace SRH.Application.DTO.dbo
{
    public record CreateRoomDto
    {
        public string NumeroHabitacion { get; init; }
        public int RoomCategoryId { get; init; }
        public int FloorId { get; }
        public String Type { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}