using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetAllFloorCreateResponse : BaseResponse
    {
      
        public List<Models.FloorCreateModels> Data { get; set; }
    }
}
