namespace SRH.Application.DTO.dbo
{
    public record CreateReservationDTO
    {
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public int ReservationStatus { get; init; }
        public decimal TotalAmount { get; init; }
        public int UserId { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreatedAt { get; init; }
        public String Status { set; get; }
    }
}