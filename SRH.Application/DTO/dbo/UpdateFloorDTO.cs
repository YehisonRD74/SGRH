namespace SGRH.Application.DTO.dbo
{
    public record UpdateFloorDto
    {
        public int Id { get; init; }
        public int NumeroPiso { get; init; }
    }
}