using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Services;

public class ReservationService : BaseService<ReservationService>, IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IConfiguration _configuration;

    public ReservationService(IReservationRepository reservationRepository, ILogger<ReservationService> logger,
        IConfiguration configuration) : base(logger)
    {
        _reservationRepository = reservationRepository;
        _configuration = configuration;
    }

    public async Task<OperationResult> GetReservation()
    {
        try
        {
            var result = await _reservationRepository.GetAllAsync();
            if (!result.IsSuccess)
            {
                LogError("Error al obtener la reserva: {0}", result.Message);
            }
            else
            {
                LogInformation("Reservas obtenidas correctamente.");
            }
            return result;
        }
        catch (Exception ex)
        {
            LogError(ex, "Excepción al obtener la reserva.");
            return OperationResult.Failure($"Error al obtener la reserva: {ex.Message}");
        }
    }

    public async Task<OperationResult> GetByIdReservation(int id, GetActiveReservationByIdDto dto)
    {
        try
        {
            var result = await _reservationRepository.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                LogError("Error al obtener la reserva por ID: {0}", result.Message);
            }
            else
            {
                LogInformation("Reserva con ID {0} obtenida correctamente. DTO: {@dto}", id, dto);
            }
            return result;
        }
        catch (Exception ex)
        {
            LogError(ex, "Excepción al obtener la reserva por ID");
            return OperationResult.Failure($"Error al obtener la reserva: {ex.Message}");
        }
    }

    public async Task<OperationResult> UpDateReservation(UpDateReservationDto dto)
    {
        if (dto == null)
            return OperationResult.Failure("DTO de actualización es nulo");

        try
        {
            LogInformation("Iniciando actualización de reserva: {@dto}", dto);

            var result = await _reservationRepository.UpdateAsync(dto);

            if (!result.IsSuccess)
            {
                LogError("Error al actualizar la reserva: {0}", result.Message);
            }
            else
            {
                LogInformation("Reserva actualizada correctamente: {@dto}", dto);
            }

            return result;
        }
        catch (Exception ex)
        {
            LogError(ex, "Excepción al actualizar la reserva.");
            return OperationResult.Failure($"Error al actualizar la reserva: {ex.Message}");
        }
    }

    public async Task<OperationResult> DisableReservation(DisableReservationDto dto)
    {
        if (dto == null)
            return OperationResult.Failure("DTO de desactivación es nulo");

        try
        {
            LogInformation("Iniciando desactivación de reserva: {@dto}", dto);

            var result = await _reservationRepository.DisableAsync(dto);

            if (!result.IsSuccess)
            {
                LogError("Error al desactivar la reserva: {0}", result.Message);
            }
            else
            {
                LogInformation("Reserva desactivada correctamente: {@dto}", dto);
            }

            return result;
        }
        catch (Exception ex)
        {
            LogError(ex, "Excepción al desactivar la reserva.");
            return OperationResult.Failure($"Error al desactivar la reserva: {ex.Message}");
        }
    }

    public async Task<OperationResult> CreateReservation(CreateReservationDto dto)
    {
        if (dto == null)
            return OperationResult.Failure("DTO de creación es nulo");

        try
        {
            LogInformation("Iniciando creación de reserva: {@dto}", dto);

            var result = await _reservationRepository.AddAsync(dto);

            if (!result.IsSuccess)
            {
                LogError("Error al crear la reserva: {0}", result.Message);
            }
            else
            {
                LogInformation("Reserva creada correctamente: {@dto}", dto);
            }

            return result;
        }
        catch (Exception ex)
        {
            LogError(ex, "Excepción al crear la reserva.");
            return OperationResult.Failure($"Error al crear la reserva: {ex.Message}");
        }
    }
}
