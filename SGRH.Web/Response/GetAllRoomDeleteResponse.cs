using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetAllRoomDeleteResponse : BaseResponse
    {
        public List<RoomDeleteModels> Data { get; set; }
    }
}
