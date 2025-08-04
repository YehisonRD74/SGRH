using SGRH.Web.Models.Base;
using SGRH.Web.Models.Floor;

namespace SGRH.Web.Response
{
    public class GetAllFloorDeleteResponse : BaseResponse
    {
       

        public List<FloorDeleteModels> Data { get; set; }
    }
}
