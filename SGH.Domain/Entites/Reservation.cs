using SGRH._Domain.Base;
using SGRH._Domain.Entities;
using YourProject.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public int UserId { get; set; }
    public int CustomerId { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public User User { get; set; }
    public Customer Customer { get; set; }
    public ICollection<ReservationDetail> ReservationDetails { get; set; }
}