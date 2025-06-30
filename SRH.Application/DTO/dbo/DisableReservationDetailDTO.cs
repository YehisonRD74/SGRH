using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record DisableReservationDetailDTO
    {
        public int Id { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
