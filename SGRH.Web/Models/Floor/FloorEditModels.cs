using SGRH.Web.Models.Base;
using System.Collections.Generic;

namespace SGRH.Web.Models.Floor
{
    public class FloorEditModels : BaseModels
    {
        public int FloorNumber { get; set; }

        public FloorEditModels(int FloorNumber, int Id, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null)
            : base(Id, CreateAt, CreatedBy, UpdatedAt, UpdatedBy, IsDeleted, DeletedBy, DeletedAt)
        {
            this.FloorNumber = FloorNumber;
        }
    }


}
