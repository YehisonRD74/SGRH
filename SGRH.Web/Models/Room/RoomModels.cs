using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SGRH.Web.Models.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGRH.Web.Models.Room
{
    public class RoomModels : BaseModels
    {
        public RoomModels() : base(0, DateTime.Now, "system") { }

        public RoomModels(int Id, DateTime CreateAt, string CreatedBy, DateTime? UpdatedAt = null, string UpdatedBy = null, bool IsDeleted = false, string DeletedBy = null, DateTime? DeletedAt = null)
            : base(Id, CreateAt, CreatedBy, UpdatedAt, UpdatedBy, IsDeleted, DeletedBy, DeletedAt)
        {
        }

        public int roomId { get; set; }
        public string numeroHabitacion { get; set; }
        public string type { get; set; }
        public int floorId { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }

}