using SGRH.Web.Models.Base;
using SGRH.Web.Models.Floor;

namespace SGRH.Web.Response
{
    public class GetAllFloorEditResponse: BaseResponse
    {
       

        public List<FloorEditModels> Data { get; set; }
    }
}
