using SGRH._Domain.Entites;

namespace SGRH.Web.Respositories.Interface
{
    public interface IFloorRepository
    {
        Task<List<Floor>> GetAllAsync();
        Task<Floor> GetByIdAsync(int id);
        Task<bool> CreateAsync(Floor floor);
        Task<bool> UpdateAsync(Floor floor);
        Task<bool> DeleteAsync(int id);
    }
}
