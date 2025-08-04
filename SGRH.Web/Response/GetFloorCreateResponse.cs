using SGRH.Web.Models;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Response
{
    public class GetFloorCreateResponse 
    {


        public Models.FloorModels Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDisable { get; set; }


    }
}
