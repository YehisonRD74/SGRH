using SGRH.Web.Models.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGRH.Web.Response

{
    public class GetAllFloorCreateResponse 
    {
    
        public List<Models.FloorModels> data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
  
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDisable { get; set; }

    }
}
