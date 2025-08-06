namespace SGRH.Web.Models.Base
{
    public abstract class BaseModels
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

     
        public BaseModels() { }

        public BaseModels(int Id, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null)
        {
            this.Id = Id;
            this.CreatedAt = CreateAt;
            this.CreatedBy = CreatedBy;
            this.UpdatedAt = UpdatedAt;
            this.UpdatedBy = UpdatedBy;
            this.IsDeleted = IsDeleted;
            this.DeletedBy = DeletedBy;
            this.DeletedAt = DeletedAt;
        }
    }
}
