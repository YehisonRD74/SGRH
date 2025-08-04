using Microsoft.AspNetCore.Mvc.RazorPages;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Models
{
    public class FloorModels : BaseModels

    {public FloorModels(int floorId, DateTime createAt, string createdBy, DateTime? updatedAt = null, string updatedBy = null, bool isDeleted = false, string deletedBy = null, DateTime? deletedAt = null)
            : base(floorId, createAt, createdBy, updatedAt, updatedBy, isDeleted, deletedBy, deletedAt)
        {
            this.FloorNumber = floorId;
        }
        public int FloorNumber { get; set; }
        public DateTime CreateAt { get; internal set; }
    }
}
