using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record DisableRoomCategoryDTO
    {
        public int CategoryId { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
