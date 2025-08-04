using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetFloorCreateResponse : BaseResponse
    {
        public FloorCreateModels Data { get; set; }
    }
}
