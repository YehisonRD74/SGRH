using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetAllFloorEditResponse: BaseResponse
    {
    
        public List<FloorEditModels> Data { get; set; }
    }
}
