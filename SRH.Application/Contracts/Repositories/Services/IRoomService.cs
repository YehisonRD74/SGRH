using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SGRH._Domain.Entites;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.Services;

public interface IRoomService
{

    Task<OperationResult> GetRoomByI(int id, GetActiveRoomByIdDto dto);

    Task<OperationResult> UpDateRoom(UpdateRoomDto updateRoom);

    Task<OperationResult> DisableRoom(DisableRoomDto disableRoomDto);
    Task<OperationResult> CreateRoom(CreateRoomDto createRoomDto);

}