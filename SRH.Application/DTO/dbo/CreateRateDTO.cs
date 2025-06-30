using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.DTO.dbo
{
    public record CreateRateDTO
    {     
            public string Season { get; init; }
            public decimal RatePrice { get; init; }
            public int CategoryId { get; init; }
        
    }
}
