using SGRH._Domain.Entities;
using SRH.Application.DTO;

namespace SRH.Application.Interfaces;

public interface IReservationService
{
    Task<List<Reservation>> GetAllReservationsAsync();
    Task<List<Reservation>> GetAllReservationbyidAsync(int userId);
    Task CreateReservationAsync( ReservationDto dto);
    Task UpdateReservationAsync(ReservationDto  dto);
    Task DeleteReservationAsync(int id);
    
    
}



