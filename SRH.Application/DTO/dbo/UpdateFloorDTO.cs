namespace SGRH.Application.DTO.dbo
{
    public record UpdateFloorDTO
    {
        public int Id { get; init; }
        public int NumeroPiso { get; init; }
    }
}