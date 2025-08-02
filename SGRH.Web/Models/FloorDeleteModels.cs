using SGRH.Web.Models.Base;

namespace SGRH.Web.Models
{
    public class FloorDeleteModels: BaseModels
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
    public class GetFloorDeleteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public FloorDeleteModels Data { get; set; }
    }
    public class GetAllFloorDeleteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<FloorDeleteModels> Data { get; set; }
    }
    
    
}
