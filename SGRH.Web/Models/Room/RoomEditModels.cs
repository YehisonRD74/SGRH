using SGRH.Web.Models.Base;

namespace SGRH.Web.Models.Room
{
    public class RoomEditModels : RoomModels
    {
        public RoomEditModels(int Id, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null) : base(Id, CreateAt, CreatedBy, UpdatedAt, UpdatedBy, IsDeleted, DeletedBy, DeletedAt)
        {
        }
    }
}
