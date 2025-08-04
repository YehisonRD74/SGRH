namespace SGRH.Application.DTO.dbo
{
    public record UpdateRoomDto
    {
        public int Id { get; init; }
        public string Descripcion { get; init; }
        public decimal Price { get; init; }
        public string NumeroHabitacion { get; init; }
        public int RoomCategoryId { get; init; }
        public int FloorId { get; init; }
        public String Type { get; set; }
        public string Status { get; set; }
    }
}