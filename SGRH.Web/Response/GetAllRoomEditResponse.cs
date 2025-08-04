using SGRH.Web.Models.Base;
using SGRH.Web.Models.Room;

namespace SGRH.Web.Response
{
    public class GetAllRoomEditResponse : BaseResponse
    {
        public List<RoomEditModels> Data { get; set; }
    }
}
