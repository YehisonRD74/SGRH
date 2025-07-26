using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.Contracts.Repositories.dbo;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Services;

public class FloorService : BaseService<FloorService>, IFloorService
{
    private readonly IFloorRepository _floorRepository;
    private new readonly ILogger<FloorService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IValidator<CreateFloorDto> _createFloorValidator;
    
    public FloorService(IFloorRepository floorRepository, ILogger<FloorService> logger,
        IConfiguration configuration, IValidator<CreateFloorDto> createFloorValidator)
        : base(logger)
    {
        _floorRepository = floorRepository;
        _logger = logger;
        _configuration = configuration;
        _createFloorValidator = createFloorValidator;
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

            if (floor.Data != null) return OperationResult<Floor>.Success(floor.Data, "Piso encontrado correctamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener piso por ID: {e.Message}");
            return OperationResult<Floor>.Failure("Error: " + e.Message);
        }

        return OperationResult<Floor>.Failure("Piso no encontrado");
    }

    public async Task<OperationResult<Floor>> UpDateFloor(UpdateFloorDto updateFloorDto)
    {
        try
        {
            var floor = await _floorRepository.GetFloorById(updateFloorDto.Id);

            if (floor.Data != null)
            {
                floor.Data.FloorNumber = updateFloorDto.FloorNumber;
                floor.Data.UpdatedBy = updateFloorDto.UpdatedBy;
                floor.Data.UpdatedAt = DateTime.UtcNow;
                await _floorRepository.UpdateFloor(floor);

                return OperationResult<Floor>.Success(floor.Data, "Piso actualizado exitosamente");
            }
        }
        catch (Exception e)
        {
            LogError(e, $"Error al actualizar el piso: {e.Message}");
            return OperationResult<Floor>.Failure("Error: " + e.Message);
        }
        return OperationResult<Floor>.Failure("Piso no encontrado"); 
    }

    public async Task<OperationResult<bool>> DisableFloor(DisableFloorDto? disableFloorDto)
    {
        try
        {
            if (disableFloorDto == null)
                return OperationResult<bool>.Failure("Datos de deshabilitación inválidos");

            var floor = await _floorRepository.GetFloorById(disableFloorDto.FloorId);

            floor.IsDisable = true;
            if (floor.Data != null)
            {
                floor.Data.DeletedAt = DateTime.UtcNow;
                floor.Data.DeletedBy = disableFloorDto.DisabledBy ?? "admin";
            }

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

            ValidationResult validationResult = await _createFloorValidator.ValidateAsync(createFloorDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
                LogError(new ValidationException(errors), $"Errores de validación al crear el piso: {errors}");
                return OperationResult<CreateFloorDto>.Failure($"Errores de validación: {errors}");
            }

            var floorEntity = new CreateFloorDto
            {
                FloorNumber = createFloorDto.FloorNumber,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createFloorDto.CreatedBy ?? "admin",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = createFloorDto.CreatedBy ?? "admin",
                IsDeleted = false
            };

            await _floorRepository.CreateFloor(floorEntity);
            return OperationResult<CreateFloorDto>.Success(createFloorDto, "Piso creado exitosamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al crear el piso: {e.Message}");
            return OperationResult<CreateFloorDto>.Failure("Error: " + e.Message);
        }
    } 
}
