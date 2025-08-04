using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.Services
{
    public interface IRoomService
    {
        Task<OperationResult<IEnumerable<GetActiveRoomDto>>> GetAllRoom();

        Task<OperationResult<GetActiveRoomDto>> GetRoomById(int id);

        Task<OperationResult<GetActiveRoomDto>> UpdateRoom(UpdateRoomDto updateRoomDto);

        Task<OperationResult<bool>> DisableRoom(DisableRoomDto disableRoomDto);

        Task<OperationResult<GetActiveRoomDto>> CreateRoom(CreateRoomDto createRoomDto);
    }
}