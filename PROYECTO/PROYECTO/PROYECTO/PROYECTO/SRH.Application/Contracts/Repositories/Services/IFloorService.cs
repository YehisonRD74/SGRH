using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SGRH.Application.DTO.dbo;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Contracts.Repositories.Services
{
    public interface IFloorService
    {
        Task<OperationResult<IEnumerable<Floor>>> GetFloor();

        Task<OperationResult<Floor>> GetFloorById(int id, GetFloorByIdDto dto);

        Task<OperationResult<Floor>> UpDateFloor(UpdateFloorDto updateFloorDto);

        Task<OperationResult<bool>> DisableFloor(DisableFloorDto disableFloorDto);

        Task<OperationResult<CreateFloorDto>> CreateFloor(CreateFloorDto createFloorDto);
    }
}