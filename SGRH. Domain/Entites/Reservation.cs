

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

        public Reservation(int id, DateTime checkInDate, DateTime checkOutDate, string status, decimal totalAmount, int customerId)
        {
            Id = id;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = status;
            TotalAmount = totalAmount;
            CustomerId = customerId;
        }
    }
}
