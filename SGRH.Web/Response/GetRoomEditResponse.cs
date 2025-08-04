using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetRoomEditResponse : BaseResponse
    {
        public RoomEditModels Data { get; set; }
    }
}
