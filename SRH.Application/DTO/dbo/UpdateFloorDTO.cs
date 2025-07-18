namespace SGRH.Application.DTO.dbo
{
    public record UpdateFloorDto
    {
        public int Id { get; init; }
        public int FloorNumber { get; set; }
        public string UpdatedBy { get; set; }
    }
}