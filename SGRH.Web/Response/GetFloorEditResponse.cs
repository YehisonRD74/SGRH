using SGRH.Web.Models.Base;
using SGRH.Web.Models.Floor;

namespace SGRH.Web.Response
{
    public class GetFloorEditResponse:BaseResponse
    {
     
        public FloorEditModels Data { get; set; }

    }
}
