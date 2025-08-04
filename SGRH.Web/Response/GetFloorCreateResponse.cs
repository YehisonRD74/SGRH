using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetFloorCreateResponse : BaseResponse
    {
        public Models.FloorModels? Data { get; set; }
    }
}