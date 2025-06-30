using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record CreateReservationDetailDTO
    {
        public decimal NightPrice { get; init; }
        public decimal Subtotal { get; init; }
        public int ReservationId { get; init; }
        public int RoomId { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
