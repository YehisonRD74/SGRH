namespace SRH.Application.DTO.dbo;

public record GetActiveReservation
{
    public int Id{ get; init; }
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public String Status { get; init; }
    public decimal TotalAmount { get; init; }
    public int UserId { get; init; }
    

   
  
}