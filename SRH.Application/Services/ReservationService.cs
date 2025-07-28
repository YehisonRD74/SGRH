using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.Contracts.Repositories.Services;
using SGRH.Application.DTO.reservations;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Application.Services;

public class ReservationService : BaseService<ReservationService>, IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private new readonly ILogger<ReservationService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IValidator<CreateReservationDto> _createReservationValidator;

    public ReservationService(
        IReservationRepository reservationRepository,
        ILogger<ReservationService> logger,
        IConfiguration configuration,
        IValidator<CreateReservationDto> createReservationValidator)
        : base(logger)
    {
        _reservationRepository = reservationRepository;
        _logger = logger;
        _configuration = configuration;
        _createReservationValidator = createReservationValidator;
    }

    public async Task<OperationResult<IEnumerable<Reservation>>> GetReservation()
    {
        try
        {
            var reservations = await _reservationRepository.GetAllResevation();
            if (reservations != null && reservations.Any())
                return OperationResult<IEnumerable<Reservation>>.Success(reservations, "Listado de reservas obtenido correctamente");

            return OperationResult<IEnumerable<Reservation>>.Failure("No se encontraron reservas");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al obtener las reservas: {e.Message}");
            return OperationResult<IEnumerable<Reservation>>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Reservation>> GetReservationById(int id, GetActiveReservationDto dto)
    {
        try
        {
            var result = await _reservationRepository.GetReservationById(id);
            if (result.Data != null && result.Data.IsActive == dto.IsActive)
            {
                return OperationResult<Reservation>.Success(result.Data, "Reserva obtenida correctamente");
            }

            return OperationResult<Reservation>.Failure("Reserva no encontrada o no activa");
        }
        catch (Exception e)
        {
            LogError(e, $"Error en GetReservationById: {e.Message}");
            return OperationResult<Reservation>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto createReservationDto)
    {
        try
        {
            if (createReservationDto == null)
                return OperationResult<Reservation>.Failure("Datos de creación inválidos");

            ValidationResult validationResult = await _createReservationValidator.ValidateAsync(createReservationDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
                LogError(new ValidationException(errors), $"Errores de validación al crear la reserva");
                return OperationResult<Reservation>.Failure($"Errores de validación: {errors}");
            }

            var result = await _reservationRepository.CreateReservation(createReservationDto);
            if (!result.IsSuccess || result.Data == null)
                return OperationResult<Reservation>.Failure(result.Message);

            return OperationResult<Reservation>.Success(result.Data, "Reserva creada exitosamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Excepción en CreateReservation: {e.Message}");
            return OperationResult<Reservation>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto dto)
    {
        try
        {
            var existing = await _reservationRepository.GetReservationById(dto.ReservationId);
            if (existing.Data == null)
                return OperationResult<Reservation>.Failure("Reserva no encontrada");

            // Aquí podrías mapear cambios si se trabaja directamente con la entidad
            existing.Data.CheckInDate = dto.CheckInDate;
            existing.Data.CheckOutDate = dto.CheckOutDate;
            existing.Data.Status = dto.Status;
            existing.Data.TotalAmount = dto.TotalAmount;
            existing.Data.UpdatedAt = DateTime.UtcNow;

            var result = await _reservationRepository.UpdateReservation(dto);
            if (!result.IsSuccess || result.Data == null)
                return OperationResult<Reservation>.Failure(result.Message);

            return OperationResult<Reservation>.Success(result.Data, "Reserva actualizada exitosamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Error al actualizar la reserva: {e.Message}");
            return OperationResult<Reservation>.Failure("Error: " + e.Message);
        }
    }

    public async Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto)
    {
        try
        {
            if (disableReservationDto == null)
                return OperationResult<Reservation>.Failure("Datos inválidos");

            var existing = await _reservationRepository.GetReservationById(disableReservationDto.ReservationId);
            if (existing.Data == null)
                return OperationResult<Reservation>.Failure("Reserva no encontrada");

            existing.Data.IsActive = false;
            existing.Data.UpdatedAt = DateTime.UtcNow;

            var result = await _reservationRepository.DisableReservation(disableReservationDto);
            if (!result.IsSuccess || result.Data == null)
                return OperationResult<Reservation>.Failure(result.Message);

            return OperationResult<Reservation>.Success(result.Data, "Reserva deshabilitada correctamente");
        }
        catch (Exception e)
        {
            LogError(e, $"Excepción en DisableReservation: {e.Message}");
            return OperationResult<Reservation>.Failure("Error: " + e.Message);
        }
    }
}
