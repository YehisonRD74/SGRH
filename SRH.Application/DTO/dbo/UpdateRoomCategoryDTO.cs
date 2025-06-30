using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record class UpdateRoomCategoryDTO
    {
        public int CategoryId { get; init; }
        public string CategoryName { get; init; }
        public string Description { get; init; }
        public decimal BaseRate { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
