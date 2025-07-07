using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH.Application.DTO.dbo;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

public class RoomService : BaseService<RoomService>, IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomService> _logger;
    private readonly IConfiguration _configuration;

    public RoomService(IRoomRepository roomRepository, ILogger<RoomService> logger, IConfiguration configuration)
    {
        _roomRepository = roomRepository;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<OperationResult> GetRoom()
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            operationResult = await _roomRepository.GetAllAsync();
            if (!operationResult.IsSuccess)
            {
               LogError("Ocurrió un error al obtener las habitaciones: {Message}", operationResult.Message);
            }
            else
            {
                LogInformation("Las habitaciones fueron obtenidas correctamente.");
            }
        }
        catch (Exception e)
        {
          LogError(e, "Error al obtener las habitaciones: {Message}", e.Message);
            operationResult = OperationResult.Failure($"Error al obtener las habitaciones: {e.Message}");
        }

        return operationResult;
    }

    public async Task<OperationResult> GetRoomById(int id, GetRoomByIdDto dto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            operationResult = await _roomRepository.GetByIdAsync(id);
            if (!operationResult.IsSuccess)
            {
                LogError("Ocurrió un error al obtener la habitación con ID {Id}: {Message}", id, operationResult.Message);
            }
            else
            {
               LogInformation("La habitación con ID {Id} fue obtenida correctamente: {@Dto}", id, dto);
            }
        }
        catch (Exception e)
        {
         LogError(e, "Error al obtener la habitación: {Message}", e.Message);
            operationResult = OperationResult.Failure($"Error al obtener la habitación: {e.Message}");
        }

        return operationResult;
    }

    public async Task<OperationResult> UpdateRoom(UpdateRoomDto updateRoomDto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            if (updateRoomDto == null)
            {
                return OperationResult.Failure("No se pudo actualizar la habitación porque los datos son nulos.");
            }

           LogInformation("Iniciando actualización de la habitación: {@Dto}", updateRoomDto);
            operationResult = await _roomRepository.UpdateAsync(updateRoomDto);

            if (!operationResult.IsSuccess)
            {
                _logger.LogError("Ocurrió un error al actualizar la habitación: {Message}", operationResult.Message);
            }
            else
            {
                _logger.LogInformation("La habitación fue actualizada correctamente: {@Dto}", updateRoomDto);
            }
        }
        catch (Exception e)
        {
            LogError(e, "Error al actualizar la habitación: {Message}", e.Message);
            operationResult = OperationResult.Failure($"Error al actualizar la habitación: {e.Message}");
        }

        return operationResult;
    }

    public Task<OperationResult> GetRoomByI(int id, GetActiveRoomByIdDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpDateRoom(UpdateRoomDto updateRoom)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> UpDateRoomr(UpdateRoomDto updateRoom)
    {
        throw new NotImplementedException();
    }

    Task<OperationResult> IRoomService.DisableRoom(DisableRoomDto disableRoomDto)
    {
        return DisableRoom(disableRoomDto);
    }

    Task<OperationResult> IRoomService.CreateRoom(CreateRoomDto createRoomDto)
    {
        return CreateRoom(createRoomDto);
    }

    public async Task<OperationResult> DisableRoom(DisableRoomDto disableRoomDto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            if (disableRoomDto == null)
            {
                return OperationResult.Failure("No se pudo desactivar la habitación porque los datos son nulos.");
            }

            LogInformation("Iniciando desactivación de la habitación: {@Dto}", disableRoomDto);
            operationResult = await _roomRepository.DisableAsync(disableRoomDto);

            if (!operationResult.IsSuccess)
            {
                LogError("Ocurrió un error al desactivar la habitación: {Message}", operationResult.Message);
            }
            else
            {
               LogInformation("La habitación fue desactivada correctamente: {@Dto}", disableRoomDto);
            }
        }
        catch (Exception e)
        {
            LogError(e, "Error al desactivar la habitación: {Message}", e.Message);
            operationResult = OperationResult.Failure($"Error al desactivar la habitación: {e.Message}");
        }

        return operationResult;
    }

    public async Task<OperationResult> CreateRoom(CreateRoomDto createRoomDto)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            if (createRoomDto == null)
            {
                return OperationResult.Failure("No se pudo crear la habitación porque los datos son nulos.");
            }

           LogInformation("Iniciando creación de la habitación: {@Dto}", createRoomDto);
            operationResult = await _roomRepository.AddAsync(createRoomDto);

            if (!operationResult.IsSuccess)
            {
                LogError("Ocurrió un error al crear la habitación: {Message}", operationResult.Message);
            }
            else
            {
               LogInformation("La habitación fue creada correctamente: {@Dto}", createRoomDto);
            }
        }
        catch (Exception e)
        {
           LogError(e, "Error al crear la habitación: {Message}", e.Message);
            operationResult = OperationResult.Failure($"Error al crear la habitación: {e.Message}");
        }

        return operationResult;
    }
}

public class GetRoomByIdDto
{
}
