namespace SRH.Application.DTO.dbo
{
    public class CreateReservationDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }


        public CreateReservationDto()
        {
        }
    }
}