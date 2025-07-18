using System.ClientModel.Primitives;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Persistences.Base;
using SGRH.Persistences.Context;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.DTO.dbo;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using SGRH._Domain.Entities;


namespace SGRH.Persistences.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly SGRHContext _context;

        public ReservationRepository(SGRHContext context, ILogger<Reservation>? logger)
            : base(logger)
        {
            _context = context;
        }

        public async Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto? createReservationDto)
{
    try
    {
        // ✅ Validar que el DTO no sea nulo primero
        if (createReservationDto == null)
        {
            return new OperationResult<Reservation>
            {
                IsSuccess = false,
                Message = "Datos inválidos",
                Data = null!
            };
        }

        // ✅ Validar que el usuario exista
        var userExists = await _context.User.AnyAsync(u => u.Id == createReservationDto.UserId);
        if (!userExists)
        {
            return new OperationResult<Reservation>
            {
                IsSuccess = false,
                Message = $"El usuario con ID {createReservationDto.UserId} no existe.",
                Data = null!
            };
        }

        var reservation = new Reservation
        {
            CustomerId = createReservationDto.CustomerId,
            CreatedAt = createReservationDto.CreatedAt,
            Status = createReservationDto.Status,
            CreatedBy = createReservationDto.CreatedBy,
            CheckInDate = createReservationDto.CheckInDate,
            CheckOutDate = createReservationDto.CheckOutDate,
            TotalAmount = createReservationDto.TotalAmount,
            UserId = createReservationDto.UserId // ✅ Esta línea es esencial
        };

        await _context.Reservation.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return new OperationResult<Reservation>
        {
            IsSuccess = true,
            Message = "Reserva Creada Exitosamente",
            Data = reservation
        };
    }
    catch (Exception ex)
    {
        var innerMessage = ex.InnerException?.Message ?? ex.Message;

        LogError(ex, "Error al crear la reserva");
        return new OperationResult<Reservation>
        {
            IsSuccess = false,
            Message = $"Error al crear la reserva: {innerMessage}",
            Data = null!
        };
    }
}

        public async Task<OperationResult<Reservation>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _context.Reservation.FindAsync(id);

                if (reservation == null)
                    return OperationResult<Reservation>.Failure("Reserva no encontrada");

                return OperationResult<Reservation>.Success(reservation, "Reserva encontrada exitosamente");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al obtener la reserva por ID");
                return OperationResult<Reservation>.Failure($"Error al buscar reserva: {ex.Message}");
            }
        }


        public async Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto)
        {
            try
            {
                var reservation = await _context.Reservation.FindAsync(updateReservationDto.ReservationId);
                if (reservation == null)
                    return OperationResult<Reservation>.Failure("Reserva no encontrada");

                reservation.Status = updateReservationDto.Status;
                reservation.CheckInDate = updateReservationDto.CheckInDate;
                reservation.CheckOutDate = updateReservationDto.CheckOutDate;
                reservation.UpdatedBy = updateReservationDto.UpdatedBy;
                reservation.UpdatedAt = DateTime.UtcNow;

                _context.Reservation.Update(reservation);
                await _context.SaveChangesAsync();

                return OperationResult<Reservation>.Success(reservation, "Reserva actualizada exitosamente");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al actualizar la reserva");
                return OperationResult<Reservation>.Failure($"Error al actualizar reserva: {ex.Message}");
            }
        }



        public async Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto)
        {
            try
            {
                var reservation = await _context.Reservation.FindAsync(disableReservationDto.ReservationId);
                if (reservation == null)
                    return OperationResult<Reservation>.Failure("Reserva no encontrada");

                reservation.IsActive = false; 

                _context.Reservation.Update(reservation);
                await _context.SaveChangesAsync();

                return OperationResult<Reservation>.Success(reservation, "Reserva deshabilitada exitosamente");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error al deshabilitar la reserva");
                return OperationResult<Reservation>.Failure($"Error al deshabilitar reserva: {ex.Message}");
            }
        }



        public async Task<OperationResult<IEnumerable<Reservation>>> GetAllReservation(Expression<Func<Reservation, bool>>? predicate = null)
        {
            try
            {
                IQueryable<Reservation> query = _context.Reservation
                    .Include(r => r.Customer)
                    .Include(r => r.User)
                    .Include(r => r.ReservationDetails);

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var list = await query.ToListAsync();

                return new OperationResult<IEnumerable<Reservation>>
                {
                    IsSuccess = true,
                    Message = list.Any() ? "Reservas obtenidas correctamente." : "No hay reservas disponibles.",
                    Data = list
                };
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;

                return new OperationResult<IEnumerable<Reservation>>
                {
                    IsSuccess = false,
                    Message = $"Error al obtener reservas: {error}",
                    Data = null
                };
            }
        }



    }
}