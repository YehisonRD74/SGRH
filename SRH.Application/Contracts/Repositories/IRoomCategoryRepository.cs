using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using SGRH._Domain.Base;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories
{
    

    namespace SGM.Application.Contracts.Repositories
    {
        public interface IRoomCategoryRepository
        {
            Task<OperationResult> AddAsync(CreateRoomCategoryDTO dto);
            Task<OperationResult> GetAllAsync();
        }
    }

}
