using SGRH.Web.Models;
using SGRH.Web.Models.Floor;

namespace SGRH.Web.Services.Interface
{
    public interface IFloorService
    {
        Task<List<FloorModels>> GetAllAsync();
        Task<FloorModels> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateFloor model);
        Task<bool> UpdateAsync(FloorEditModels model);
        Task<bool> DeleteAsync(int id);
    }
}
