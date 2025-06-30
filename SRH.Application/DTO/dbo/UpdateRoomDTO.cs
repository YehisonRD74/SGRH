namespace SGRH.Application.DTO.dbo
{
    public record UpdateRoomDTO
    { 
        public int Id { get; init; }
        public string Descripcion { get; init; }
        public decimal Price { get; init; }
        public int NumeroHabitacion { get; init; }
        public string Estado { get; init; }
        public int RoomCategoryId { get; init; }
        public int FloorId { get; init; }
        public String Type { get; set; }
    }
}