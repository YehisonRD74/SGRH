using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetAllFloorEditResponse: BaseResponse
    {
        public GetAllFloorEditResponse(bool isSuccess, string message, string updatedBy, DateTime updatedAt, bool isDisable = false) : base(isSuccess, message, updatedBy, updatedAt, isDisable)
        {
        }

        public List<FloorEditModels> Data { get; set; }
    }
}
