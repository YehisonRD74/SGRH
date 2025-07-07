using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.Services;

public interface IFloorService
{
    Task<OperationResult> GetFloor();

    Task<OperationResult> GetFloorByI(int id, GetFloorByIdDto dto);

    Task<OperationResult> UpDateFloor(UpdateFloorDto upDateFloorDto);

    Task<OperationResult> DisableFloor(DisableFloorDto? disableFloorDto);
    Task<OperationResult> CreateFloor(CreateFloorDto? createFloorDto);
}
