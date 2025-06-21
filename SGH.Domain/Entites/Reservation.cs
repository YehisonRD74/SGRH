using YourProject.Domain.Entities;

namespace SGRH._Domain.Entities
{
    public class Reservation
    {
        public int Id { get;  set; }
        public DateTime CheckInDate { get;  set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } 
        public decimal TotalAmount { get; set; }
        
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } 
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
        public void UpdateDates(DateTime checkIn, DateTime checkOut)
        {
            CheckInDate = checkIn;
            CheckOutDate = checkOut;
        }

        public void UpdateStatus(string status)
        {
            Status = status;
        }

        public void UpdateTotalAmount(decimal amount)
        {
            TotalAmount = amount;
        }
    }
}