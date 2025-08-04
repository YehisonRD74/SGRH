using Microsoft.AspNetCore.Mvc.RazorPages;
using SGRH.Web.Models.Base;

namespace SGRH.Web.Models
{
    public class FloorModels { 

   
        public int FloorNumber { get; set; }
        public int Id { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
    public string CreatedBy { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
        public string UpdatedBy { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public string DeletedBy { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }

        public FloorModels(int floorNumber,int id, DateTime createdAt, string createdBy, DateTime? updatedAt = null, string updatedBy = null, bool isDeleted = false, string deletedBy = null, DateTime? deletedAt = null)
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


    
    }
    

}

