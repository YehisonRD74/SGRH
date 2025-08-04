using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetRoomCreateResponse : BaseResponse
    {
        public RoomModels Data { get; set; }

    }
}
