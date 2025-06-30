using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{

    public record UpdateReservationDetailDTO
    {
        public int Id { get; init; }
        public decimal NightPrice { get; init; }
        public decimal Subtotal { get; init; }
        public int ReservationId { get; init; }
        public int RoomId { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
