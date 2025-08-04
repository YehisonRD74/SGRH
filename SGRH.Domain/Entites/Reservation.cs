using SGRH._Domain.Base;
using SGRH._Domain.Entities;

namespace SGRH._Domain.Entites;

public class Reservation: AuditEntity
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public int UserId { get; set; }
    public int CustomerId { get; set; }
    public bool IsActive { get; set; } = true;
    public User? User { get; set; }
    public ICollection<ReservationDetail>? ReservationDetails { get; set; }
}