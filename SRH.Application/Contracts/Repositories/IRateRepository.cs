using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH.Application.Contracts.Repositories
{
    public interface IRateRepository
    {
        Task<OperationResult> AddAsync(CreateRateDTO createRateDTO);

        Task<OperationResult> UpdateAsync(UpdateRateDTO updateRateDTO);

        Task<OperationResult> DisableAsync(DisableRateDTO disableRateDTO);

        Task<OperationResult> GetAllAsync();
    }
}
