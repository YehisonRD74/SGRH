using SGRH.Web.Models.Room;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRH.Web.Services.Interface
{
    public interface IRoomService
    {
        Task<List<RoomModels>> GetAllRoomsAsync();
        Task<RoomModels?> GetRoomByIdAsync(int id);
        Task<bool> CreateRoomAsync(RoomModels room);
        Task<bool> UpdateRoomAsync(int id, RoomEditModels room);
        Task<bool> DisableRoomAsync(int id);
    }
}
