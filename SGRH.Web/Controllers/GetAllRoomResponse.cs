using SGRH.Web.Models.Base;
using SGRH.Web.Models.Floor;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Controllers
{
    public class GetAllRoomResponse: BaseResponse
    {
        public List<RoomModels> Data { get; set; }
    }
    
}