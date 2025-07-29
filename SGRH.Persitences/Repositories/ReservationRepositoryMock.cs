// using Microsoft.EntityFrameworkCore;
// using SGRH._Domain.Base;
// using SGRH._Domain.Entites;
// using SGRH.Persitences.Tests.Context;
// using SRH.Application.Contracts.Repositories.dbo;
// using SRH.Application.DTO.dbo;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Text;
// using System.Threading.Tasks;
//
//
// namespace SGRH.Persitences.Tests.Repositories
// {
//     public class ReservationRepositoryMorck : IReservationRepository
//     {
//         public ReservationContext _Context { get; }
//
//         public ReservationRepositoryMorck(ReservationContext context)
//         {
//             _Context = context;
//         }
//     
//         public async Task<OperationResult<Reservation>> CreateReservation(CreateReservationDto? createReservationDto)
//         {
//             try
//             {
//
//                 if (createReservationDto == null)
//                 {
//                     return new OperationResult<Reservation>
//                     {
//                         IsSuccess = false,
//                         Message = "Datos inválidos",
//                         Data = null!
//                     };
//                 }
//
//                 var userExists = await _Context.User.AnyAsync(u => u.Id == createReservationDto.UserId);
//                 if (!userExists)
//                 {
//                     return new OperationResult<Reservation>
//                     {
//                         IsSuccess = false,
//                         Message = $"El usuario con ID {createReservationDto.UserId} no existe.",
//                         Data = null!
//                     };
//                 }
//
//                 var reservation = new Reservation
//                 {
//                     CustomerId = createReservationDto.CustomerId,
//                     CreatedAt = createReservationDto.CreatedAt,
//                     Status = createReservationDto.Status,
//                     CreatedBy = createReservationDto.CreatedBy,
//                     CheckInDate = createReservationDto.CheckInDate,
//                     CheckOutDate = createReservationDto.CheckOutDate,
//                     TotalAmount = createReservationDto.TotalAmount,
//                     UserId = createReservationDto.UserId
//                 };
//
//                 await _Context.Reservation.AddAsync(reservation);
//                 await _Context.SaveChangesAsync();
//
//                 return new OperationResult<Reservation>
//                 {
//                     IsSuccess = true,
//                     Message = "Reserva Creada Exitosamente",
//                     Data = reservation
//                 };
//             }
//             catch (Exception ex)
//             {
//                 var innerMessage = ex.InnerException?.Message ?? ex.Message;
//
//               
//                 return new OperationResult<Reservation>
//                 {
//                     IsSuccess = false,
//                     Message = $"Error al crear la reserva: {innerMessage}",
//                     Data = null!
//                 };
//             }
//         }
//
//         public async Task<OperationResult<Reservation>> GetReservationById(int id)
//         {
//             try
//             {
//                 var reservation = await _Context.Reservation.FindAsync(id);
//
//                 if (reservation == null)
//                     return OperationResult<Reservation>.Failure("Reserva no encontrada");
//
//                 return OperationResult<Reservation>.Success(reservation, "Reserva encontrada exitosamente");
//             }
//             catch (Exception ex)
//             {
//                
//                 return OperationResult<Reservation>.Failure($"Error al buscar reserva: {ex.Message}");
//             }
//         }
//
//
//         public async Task<OperationResult<Reservation>> UpdateReservation(UpDateReservationDto updateReservationDto)
//         {
//             try
//             {
//                 var reservation = await _Context.Reservation.FindAsync(updateReservationDto.ReservationId);
//                 if (reservation == null)
//                     return OperationResult<Reservation>.Failure("Reserva no encontrada");
//
//                 reservation.Status = updateReservationDto.Status;
//                 reservation.CheckInDate = updateReservationDto.CheckInDate;
//                 reservation.CheckOutDate = updateReservationDto.CheckOutDate;
//                 reservation.UpdatedBy = updateReservationDto.UpdatedBy;
//                 reservation.UpdatedAt = DateTime.UtcNow;
//
//                 _Context.Reservation.Update(reservation);
//                 await _Context.SaveChangesAsync();
//
//                 return OperationResult<Reservation>.Success(reservation, "Reserva actualizada exitosamente");
//             }
//             catch (Exception ex)
//             {
//               
//                 return OperationResult<Reservation>.Failure($"Error al actualizar reserva: {ex.Message}");
//             }
//         }
//
//
//
//         public async Task<OperationResult<Reservation>> DisableReservation(DisableReservationDto disableReservationDto)
//         {
//             try
//             {
//                 var reservation = await _Context.Reservation.FindAsync(disableReservationDto.ReservationId);
//                 if (reservation == null)
//                     return OperationResult<Reservation>.Failure("Reserva no encontrada");
//                 reservation.IsActive = false;
//
//                 _Context.Reservation.Update(reservation);
//                 await _Context.SaveChangesAsync();
//
//                 return OperationResult<Reservation>.Success(reservation, "Reserva deshabilitada exitosamente");
//             }
//             catch (Exception ex)
//             {
//               
//                 return OperationResult<Reservation>.Failure($"Error al deshabilitar reserva: {ex.Message}");
//             }
//         }
//
//
//
//         public async Task<OperationResult<IEnumerable<Reservation>?>> GetAllReservation(Expression<Func<Reservation, bool>>? predicate = null)
//         {
//             try
//             {
//                 IQueryable<Reservation> query = _Context.Reservation
//                     .Include(r => r.Customer)
//                     .Include(r => r.User)
//                     .Include(r => r.ReservationDetails);
//
//                 if (predicate != null)
//                 {
//                     query = query.Where(predicate);
//                 }
//
//                 var list = await query.ToListAsync();
//
//                 return new OperationResult<IEnumerable<Reservation>?>
//                 {
//                     IsSuccess = true,
//                     Message = list.Any() ? "Reservas obtenidas correctamente." : "No hay reservas disponibles.",
//                     Data = list
//                 };
//             }
//             catch (Exception ex)
//             {
//                 var error = ex.InnerException?.Message ?? ex.Message;
//
//                 return new OperationResult<IEnumerable<Reservation>?>
//                 {
//                     IsSuccess = false,
//                     Message = $"Error al obtener reservas: {error}",
//                     Data = null
//                 };
//             }
//         }
//
//
//
//     }
// }
//
