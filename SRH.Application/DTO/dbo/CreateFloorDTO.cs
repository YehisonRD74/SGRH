namespace SGRH.Application.DTO.dbo
{
    public record CreateFloorDto
    {
        public int FloorId { get; init; }
        public int FloorNumber { get; init; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}