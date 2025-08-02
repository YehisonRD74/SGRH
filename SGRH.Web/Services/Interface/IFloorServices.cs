using SGRH.Web.Models;

namespace SGRH.Web.Services.Interface
{
    public interface IFloorService
    {
        Task<List<FloorModels>> GetAllAsync();
        Task<FloorModels> GetByIdAsync(int id);
        Task<bool> CreateAsync(FloorCreateModels model);
        Task<bool> UpdateAsync(FloorEditModels model);
        Task<bool> DeleteAsync(int id);
    }
}
