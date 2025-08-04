using SGRH.Application.DTO.user;

namespace SGRH.Application.DTO.reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string? Status { get; set; }
    public decimal TotalAmount { get; set; }
    public UserDto? User { get; set; }
    public bool IsActive { get; set; }
}
