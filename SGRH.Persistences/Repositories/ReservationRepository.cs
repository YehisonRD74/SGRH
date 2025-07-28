using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;
using SGRH._Domain.Entites;
using SGRH.Application.Contracts.Repositories.Services;
using SGRH.Application.DTO.reservations;
using SGRH.Application.DTO.user;
using SGRH.Persistences.Context;
using SRH.Application.Contracts.Repositories.dbo;
using SRH.Application.DTO.dbo;

namespace SGRH.Persistences.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly SGRHContext _context;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(SGRHContext context, ILogger<ReservationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto? dto)
        {
            try
            {
                if (dto == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Datos inválidos", Data = null! };

                var reservation = new Reservation
                {
                    CheckInDate = dto.CheckInDate,
                    CheckOutDate = dto.CheckOutDate,
                    Status = dto.Status,
                    TotalAmount = dto.TotalAmount,
                    UserId = dto.UserId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Reservation.AddAsync(reservation);
                await _context.SaveChangesAsync();

                return new OperationResult<Reservation>
                {
                    IsSuccess = true,
                    Message = "Reserva creada exitosamente",
                    Data = reservation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CreateReservation");
                return new OperationResult<Reservation>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }

        public async Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto? dto)
        {
            try
            {
                if (dto == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Datos inválidos", Data = null! };

                var existing = await _context.Reservation.FindAsync(dto.ReservationId);
                if (existing == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Reserva no encontrada", Data = null! };

                existing.CheckInDate = dto.CheckInDate;
                existing.CheckOutDate = dto.CheckOutDate;
                existing.Status = dto.Status;
                existing.TotalAmount = dto.TotalAmount;
                existing.UpdatedAt = DateTime.UtcNow;

                _context.Reservation.Update(existing);
                await _context.SaveChangesAsync();

                return new OperationResult<Reservation>
                {
                    IsSuccess = true,
                    Message = "Reserva actualizada correctamente",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateReservation");
                return new OperationResult<Reservation>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }

        public async Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto? dto)
        {
            try
            {
                if (dto == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Datos inválidos", Data = null! };

                var reservation = await _context.Reservation.FindAsync(dto.ReservationId);
                if (reservation == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Reserva no encontrada", Data = null! };

                reservation.IsActive = false;
                reservation.UpdatedAt = DateTime.UtcNow;

                _context.Reservation.Update(reservation);
                await _context.SaveChangesAsync();

                return new OperationResult<Reservation>
                {
                    IsSuccess = true,
                    Message = "Reserva deshabilitada",
                    Data = reservation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DisableReservation");
                return new OperationResult<Reservation>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }

        public async Task<OperationResult<Reservation>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _context.Reservation
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reservation == null)
                    return new OperationResult<Reservation> { IsSuccess = false, Message = "Reserva no encontrada", Data = null! };

                return new OperationResult<Reservation>
                {
                    IsSuccess = true,
                    Message = "Reserva encontrada",
                    Data = reservation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetReservationById");
                return new OperationResult<Reservation>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null!
                };
            }
        }


        public async Task<IEnumerable<Reservation>> GetAllResevation(Expression<Func<Reservation, bool>>? filter)
        {
            return await _context.Reservation.ToListAsync();
        }
    }
}
