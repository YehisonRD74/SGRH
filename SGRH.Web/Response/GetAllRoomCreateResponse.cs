using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetAllRoomCreateResponse : BaseResponse
    {
        public List<RoomModels> Data { get; set; }
    }
}
