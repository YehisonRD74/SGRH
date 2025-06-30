using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record UpDateReservationDTO
    {
        public int ReservationId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public String Status { get; init; }
        public decimal TotalAmount { get; init; }
        public int UserId { get; init; }
        public DateTime UpdateAT { get; init; }



    }
}
