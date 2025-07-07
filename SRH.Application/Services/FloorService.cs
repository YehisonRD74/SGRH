using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
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
        : base(logger) // Si `BaseService` tiene constructor con logger
    {
        _floorRepository = floorRepository;
        this.logger = logger;
        _configuration = configuration;
    }

    public async Task<OperationResult> GetFloor()
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            operationResult = await _floorRepository.GetAllAsync();
            if (!operationResult.IsSuccess)
            {
                LogError($"Ocurrió un error al obtener los pisos: {operationResult.Message}");
            }
            LogInformation("Los pisos fueron obtenidos correctamente", operationResult);
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener los pisos: {e.Message}");
            operationResult = OperationResult.Failure($"Error al obtener los pisos: {e.Message}");
        }
        return operationResult;
    }

    public async Task<OperationResult> GetFloorByI(int id, GetFloorByIdDto dto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            operationResult = await _floorRepository.GetByIdAsync(id);
            if (!operationResult.IsSuccess)
            {
                LogError($"Ocurrió un error al obtener el piso: {operationResult.Message}");
            }
            LogInformation("El piso fue obtenido correctamente. DTO: {@dto}", dto);
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener el piso: {e.Message}");
            operationResult = OperationResult.Failure($"Error al obtener el piso: {e.Message}");
        }
        return operationResult;
    }

    public async Task<OperationResult> UpDateFloor(UpdateFloorDto upDateFloorDTO)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            LogInformation("Iniciando actualización del piso. Datos: {@upDateFloorDTO}", upDateFloorDTO);
            if (upDateFloorDTO == null)
            {
                return OperationResult.Failure("No se pudo actualizar el piso");
            }

            operationResult = await _floorRepository.UpdateAsync(upDateFloorDTO);
            if (!operationResult.IsSuccess)
            {
                LogError($"Ocurrió un error al actualizar el piso: {operationResult.Message}");
                return operationResult;
            }

            LogInformation("El piso fue actualizado correctamente. DTO: {@upDateFloorDTO}", upDateFloorDTO);
        }
        catch (Exception e)
        {
            LogError(e, $"Error al actualizar el piso: {e.Message}");
            operationResult = OperationResult.Failure($"Error al actualizar el piso: {e.Message}");
        }
        return operationResult;
    }

    public async Task<OperationResult> DisableFloor(DisableFloorDto? disableFloorDTO)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            if (disableFloorDTO == null)
            {
                return OperationResult.Failure("No se pudo desactivar el piso: objeto nulo");
            }

            operationResult = await _floorRepository.DisableAsync(disableFloorDTO);
            if (!operationResult.IsSuccess)
            {
                LogError($"Ocurrió un error al desactivar el piso: {operationResult.Message}");
                return operationResult;
            }

            LogInformation("El piso fue desactivado correctamente. DTO: {@disableFloorDTO}", disableFloorDTO);
        }
        catch (Exception e)
        {
            LogError(e, $"Error al desactivar el piso: {e.Message}");
            operationResult = OperationResult.Failure($"Error al desactivar el piso: {e.Message}");
        }
        return operationResult;
    }

    public async Task<OperationResult> CreateFloor(CreateFloorDto? createFloorDto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            if (createFloorDto == null)
            {
                return OperationResult.Failure("No se pudo crear el piso: objeto nulo");
            }

            operationResult = await _floorRepository.AddAsync(createFloorDto);
            if (!operationResult.IsSuccess)
            {
                LogError($"Ocurrió un error al crear el piso: {operationResult.Message}");
                return operationResult;
            }

            LogInformation("El piso fue creado correctamente. DTO: {@createFloorDto}", createFloorDto);
        }
        catch (Exception e)
        {
            LogError(e, $"Error al crear el piso: {e.Message}");
            operationResult = OperationResult.Failure($"Error al crear el piso: {e.Message}");
        }
        return operationResult;
    }
}
