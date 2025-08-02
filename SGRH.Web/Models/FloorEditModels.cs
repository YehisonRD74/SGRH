using System.Collections.Generic;

namespace SGRH.Web.Models
{
    public class FloorEditModels
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

    public class GetFloorEditResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public FloorEditModels Data { get; set; }
    }
    public class GetAllFloorEditResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<FloorEditModels> Data { get; set; }
    }
}
