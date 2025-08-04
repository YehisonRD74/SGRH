using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetAllFloorDeleteResponse : BaseResponse
    {
        public List<FloorDeleteModels> Data { get; set; }
    }
}
