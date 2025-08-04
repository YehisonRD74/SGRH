using SGRH.Web.Models.Base;

namespace SGRH.Web.Models.Floor
{
    public class GetCreateFloorCreateResponse : BaseModels
    {
        public int FloorNumber { get; set; }

        public GetCreateFloorCreateResponse(int FloorId, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null)
            : base(FloorId, CreateAt, CreatedBy, UpdatedAt, UpdatedBy, IsDeleted, DeletedBy, DeletedAt)
        {
            FloorNumber = FloorId;
        }

    }



}
