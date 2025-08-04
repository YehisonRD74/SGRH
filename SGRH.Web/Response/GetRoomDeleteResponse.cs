using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetRoomDeleteResponse : BaseResponse
    {
        public RoomDeleteModels Data { get; set; }
    }
}
