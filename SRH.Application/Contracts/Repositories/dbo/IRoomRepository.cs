using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<OperationResult> AddAsync(CreateRoomDTO CreateRoomDTO);
        

        Task<OperationResult> UpdateAsync(UpdateRoomDTO UpdateRoomDTO);
       
        Task<OperationResult> DisableAsync(DisableRoomDTO DisableRoomDTO);
       
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetByIdAsync(int id);

    }
}