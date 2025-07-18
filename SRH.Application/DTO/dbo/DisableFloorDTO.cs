namespace SRH.Application.DTO.dbo
{
    public record DisableFloorDto
    {
        public int FloorId { get; init; }
        public int FloorNumber { get; init; }
        public string? DisabledBy { get; init; }
    }
}