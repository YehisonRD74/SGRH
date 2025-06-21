using SGRH._Domain.Entities;

namespace SRH.Application.Contracts;

public interface IReservationRepository
{
    
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation> GetByIdAsync(int id);
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(int id);
}
        
 