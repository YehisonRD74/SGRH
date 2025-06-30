using System;
using System.Collections.Generic;
using SGRH._Domain.Base;
using YourProject.Domain.Entities;

namespace SGRH._Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } 
        public decimal TotalAmount { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 
        public ICollection<ReservationDetail> ReservationDetails { get; set; } 

        public Reservation(DateTime checkInDate, DateTime checkOutDate, string status, decimal totalAmount, int customerId)
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = status;
            TotalAmount = totalAmount;
            CustomerId = customerId;

            ReservationDetails = new List<ReservationDetail>();
        }

        protected Reservation() : base() { }

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