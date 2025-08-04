using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetFloorEditResponse:BaseResponse
    {
        public GetFloorEditResponse(bool isSuccess, string message, string updatedBy, DateTime updatedAt, bool isDisable = false) : base(isSuccess, message, updatedBy, updatedAt, isDisable)
        {
        }

        public FloorEditModels Data { get; set; }

    }
}
