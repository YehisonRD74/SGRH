namespace SGRH.Web.Models
{
    public class FloorModels
    {
        public int FloorNumber { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public FloorModels(int floorNumber, int id, DateTime createdAt, string createdBy, DateTime? updatedAt = null, string updatedBy = null, bool isDeleted = false, string deletedBy = null, DateTime? deletedAt = null)
        {
            FloorNumber = floorNumber;
            Id = id;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            IsDeleted = isDeleted;
            DeletedBy = deletedBy;
            DeletedAt = deletedAt;
        }

        public FloorModels() { }
    }
}
