using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetAllFloorDeleteResponse : BaseResponse
    {
        public GetAllFloorDeleteResponse(bool isSuccess, string message, string updatedBy, DateTime updatedAt, bool isDisable = false) : base(isSuccess, message, updatedBy, updatedAt, isDisable)
        {
        }

        public List<FloorDeleteModels> Data { get; set; }
    }
}
