namespace SRH.Application.DTO;

public class ReservationDto
{
    public int Id { get;  set; }
    public DateTime CheckInDate { get;  set; }
    public DateTime CheckOutDate { get; set; }
    public string Status { get; set; } 
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    
}