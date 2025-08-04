using SGRH.Web.Models.Base;

namespace SGRH.Web.Models
{
    public class FloorDeleteModels : BaseModels
    {
        public int FloorNumber { get; internal set; }   

        public FloorDeleteModels(int FloorNumber, int Id, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null) : base(Id, CreateAt, CreatedBy, UpdatedAt, UpdatedBy, IsDeleted, DeletedBy, DeletedAt)
        {
            this.FloorNumber = FloorNumber;
        }

      
    }

    
    
}
