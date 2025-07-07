using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.dbo
{
    public interface IRoomRepository
    {
        Task<OperationResult> AddAsync(CreateRoomDto? createRoomDto);

        Task<OperationResult> UpdateAsync(UpdateRoomDto? updateRoomDto);

        Task<OperationResult> DisableAsync(DisableRoomDto? disableRoomDto);

        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetByIdAsync(int id);
    }
}