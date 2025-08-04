

using SGRH.Web.Models;

namespace SGRH.Web.Respositories.Interface
{
    public interface IFloorRepository
    {
        Task<List<FloorModels>> GetAllAsync();
        Task<FloorModels> GetByIdAsync(int id);
        Task<bool> CreateAsync(FloorModels floor);
        Task<bool> UpdateAsync(FloorModels floor);
        Task<bool> DeleteAsync(int id);
    }
}
