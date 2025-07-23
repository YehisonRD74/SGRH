namespace SRH.Application.DTO.dbo;

public class DisableReservationDto
{
    public int ReservationId { get; set; }
    public string DisabledBy { get; set; } = "admin";
    public DateTime DisabledAt { get; set; } = DateTime.UtcNow;
}