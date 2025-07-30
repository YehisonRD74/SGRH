namespace SGRH.Web.Models
{
    public class FloorCreateModels
    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GetAllFloorCreateResponse
{
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<FloorCreateModels> Data { get; set; }
    } 
    public class GetFloorCreateResponse
{
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public FloorCreateModels Data { get; set; }
    }

}
