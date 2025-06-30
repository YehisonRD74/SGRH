using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SGRH._Domain.Entities;
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SGM.Application.Contracts.Repositories
{
    public interface IReservationDetailRepository
    {
        Task<OperationResult> AddAsync(CreateReservationDetailDTO dto);
        Task<OperationResult> UpdateAsync(UpdateReservationDetailDTO dto);
        Task<OperationResult> DisableAsync(DisableReservationDetailDTO dto);
        Task<OperationResult> GetAllAsync();
    }
}
