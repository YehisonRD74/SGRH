using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Services;

public class FloorService : BaseService<FloorService>, IFloorService
{
    private readonly IFloorRepository _floorRepository;
    private readonly ILogger<FloorService> logger;
    private readonly IConfiguration _configuration;

    public FloorService(IFloorRepository floorRepository, ILogger<FloorService> logger, IConfiguration configuration)
        : base(logger)
    {
        _floorRepository = floorRepository;
        this.logger = logger;
        _configuration = configuration;
    }

    public async Task<OperationResult<IEnumerable<Floor>>> GetFloor()
    {
        try
        {
            var floors = await _floorRepository.GetAllFloor();
            return OperationResult<IEnumerable<Floor>>.Success(floors, "Listado de pisos obtenido correctamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener los pisos: {e.Message}");
            return OperationResult<IEnumerable<Floor>>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Floor>> GetFloorById(int id, GetFloorByIdDto dto)
    {
        try
        {
            var floor = await _floorRepository.GetFloorById(id);

            if (floor == null)
                return OperationResult<Floor>.Failure($"No se encontró ningún piso con el ID {id}");

            return OperationResult<Floor>.Success(floor.Data!, "Piso encontrado correctamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener piso por ID: {e.Message}");
            return OperationResult<Floor>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Floor>> UpDateFloor(UpdateFloorDto updateFloorDto)
    {
        try
        {
            var floor = await _floorRepository.GetFloorById(updateFloorDto.Id);

            if (floor == null)
                return OperationResult<Floor>.Failure($"Piso con ID {updateFloorDto.Id} no encontrado");

            floor.Data.FloorNumber = updateFloorDto.FloorNumber;
            floor.Data.UpdatedBy = updateFloorDto.UpdatedBy ?? "admin";
            floor.Data.UpdatedAt = DateTime.UtcNow;
            await _floorRepository.UpdateFloor(floor);

            return OperationResult<Floor>.Success(floor.Data, "Piso actualizado exitosamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al actualizar el piso: {e.Message}");
            return OperationResult<Floor>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<bool>> DisableFloor(DisableFloorDto? disableFloorDto)
    {
        try
        {
            if (disableFloorDto == null)
                return OperationResult<bool>.Failure("Datos de deshabilitación inválidos");

            var floor = await _floorRepository.GetFloorById(disableFloorDto.FloorId);

            if (floor == null)
                return OperationResult<bool>.Failure($"Piso con ID {disableFloorDto.FloorId} no encontrado");

            floor.IsDisable = true;
            floor.Data.DeletedAt = DateTime.UtcNow;
            floor.Data.DeletedBy = disableFloorDto.DisabledBy ?? "admin";

            await _floorRepository.UpdateFloor(floor);

            return OperationResult<bool>.Success(true, "Piso deshabilitado correctamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al deshabilitar el piso: {e.Message}");
            return OperationResult<bool>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<CreateFloorDto>> CreateFloor(CreateFloorDto? createFloorDto)
    {
        try
        {
            if (createFloorDto == null)
                return OperationResult<CreateFloorDto>.Failure("Datos de creación inválidos");

            var floor = new CreateFloorDto
            {
                FloorNumber = createFloorDto.FloorNumber,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "admin",          
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "admin",          
                IsDeleted = false
            };


            await _floorRepository.CreateFloor(floor); 

            return OperationResult<CreateFloorDto>.Success(floor, "Piso creado exitosamente"); 
        }
        catch (Exception e)
        {
            LogError(e, $"Error al crear el piso: {e.Message}");
            return OperationResult<CreateFloorDto>.Failure("Error: " + e.Message);
        }
    }

}
