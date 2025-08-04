using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetFloorDeleteResponse : BaseResponse
    {
        public GetFloorDeleteResponse(bool isSuccess, string message, string updatedBy, DateTime updatedAt, bool isDisable = false) : base(isSuccess, message, updatedBy, updatedAt, isDisable)
        {
        }

        public FloorDeleteModels Data { get; set; }

    }
}
