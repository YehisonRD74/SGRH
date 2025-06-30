using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;
using System.Threading.Tasks;


namespace SGRH.Persistences.Repositories
{
    

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

}
