using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH._Domain.Entities;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.Contracts.Repositories.Services;
using SRH.Application.DTO.dbo;

namespace SRH.Application.Services
{
    public class ReservationService : BaseService<Reservation>, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IConfiguration _configuration;

        public ReservationService(IReservationRepository reservationRepository,
                                  ILogger<Reservation> logger,
                                  IConfiguration configuration)
            : base(logger)
        {
            _reservationRepository = reservationRepository;
            _configuration = configuration;
        }

        public async Task<OperationResult<IEnumerable<Reservation>>> GetAllReservation(Expression<Func<Reservation, bool>>? predicate = null)
        {
            try
            {
                var result = await _reservationRepository.GetAllReservation(predicate);

                if (!result.IsSuccess)
                {
                    return new OperationResult<IEnumerable<Reservation>>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    };
                }

                return new OperationResult<IEnumerable<Reservation>>
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = result.Data
                };
            }
            catch (Exception e)
            {
                LogError(e, $"Error al obtener las reservas: {e.Message}");

                return new OperationResult<IEnumerable<Reservation>>
                {
                    IsSuccess = false,
                    Message = $"Error: {e.Message}",
                    Data = null
                };
            }
        }



        public async Task<OperationResult<Reservation>> GetReservationById(int id)
        {
            try
            {
                var result = await _reservationRepository.GetReservationById(id);

                if (!result.IsSuccess || result.Data == null)
                {
                    LogError("Reserva no encontrada: {0}", result.Message);
                    return OperationResult<Reservation>.Failure(result.Message);
                }

                LogInformation("Reserva obtenida correctamente.");
                return OperationResult<Reservation>.Success(result.Data, result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en GetReservationById.");
                return OperationResult<Reservation>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto createReservationDto)
        {
            try
            {
                var result = await _reservationRepository.CreateReservation(createReservationDto);

                if (!result.IsSuccess || result.Data == null)
                {
                    LogError("Error al crear la reserva: {0}", result.Message);
                    return OperationResult<Reservation>.Failure(result.Message);
                }

                LogInformation("Reserva creada exitosamente.");
                return OperationResult<Reservation>.Success(result.Data, result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en CreateReservation.");
                return OperationResult<Reservation>.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto)
        {
            try
            {
                var result = await _reservationRepository.UpdateReservation(updateReservationDto);

                if (!result.IsSuccess || result.Data == null)
                {
                    LogError("Error al actualizar la reserva: {0}", result.Message);
                    return OperationResult<Reservation>.Failure(result.Message);
                }

                LogInformation("Reserva actualizada exitosamente.");
                return OperationResult<Reservation>.Success(result.Data, result.Message);
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en UpdateReservation.");
                return OperationResult<Reservation>.Failure($"Error: {ex.Message}");
            }
        }
        public async Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto)
        {
            try
                var result = await _reservationRepository.DisableReservation(disableReservationDto);

            if (!result.IsSuccess || result.Data == null)
            {
                LogError("Error al deshabilitar la reserva: {0}", result.Message);
                return OperationResult<Reservation>.Failure(result.Message);
            }

            {
             
                var getResult = await _reservationRepository.GetReservationById(disableReservationDto.ReservationId);

                if (!getResult.IsSuccess || getResult.Data == null)
                {
                    LogError("Reserva no encontrada: {0}", getResult.Message);
                    return OperationResult<Reservation>.Failure("Reserva no encontrada.");
                }

              
                var disableResult = await _reservationRepository.DisableReservation(disableReservationDto);

                if (!disableResult.IsSuccess || disableResult.Data == null)
                {
                    LogError("Error al deshabilitar la reserva: {0}", disableResult.Message);
                    return OperationResult<Reservation>.Failure(disableResult.Message);
                }

                LogInformation("Reserva deshabilitada exitosamente.");
                return OperationResult<Reservation>.Success(disableResult.Data, "Reserva deshabilitada exitosamente");
            }
            catch (Exception ex)
            {
                LogError(ex, "Excepción en DisableReservation.");
                return OperationResult<Reservation>.Failure($"Error: {ex.Message}");
            }
        }

    }
}
