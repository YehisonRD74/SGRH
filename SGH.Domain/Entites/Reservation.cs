using YourProject.Domain.Entities;

namespace SGRH._Domain.Entities
{
    public class Reservation
    {
        public int Id { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public string Status { get; private set; } // Confirmed, Cancelled, Pending
        public decimal TotalAmount { get; private set; }
        
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } // Relación con Cliente
        public ICollection<ReservationDetail> ReservationDetails { get; private set; } // Relación con Detalles

        public Reservation(int id, DateTime checkInDate, DateTime checkOutDate, string status, decimal totalAmount, int customerId)
        {
            Id = id;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = status;
            TotalAmount = totalAmount;
            CustomerId = customerId;

            ReservationDetails = new List<ReservationDetail>();
        }
    }
}