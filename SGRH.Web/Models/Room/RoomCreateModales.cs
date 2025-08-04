using SGRH.Web.Models.Base;

namespace SGRH.Web.Models.Room
{
    public class RoomCreateModales : RoomBase
    {
        public RoomCreateModales(int roomId, string numeroHabitacion, string type, int floorId, double price, string status)
            : base(roomId, numeroHabitacion, type, floorId, price, status)
        {
        }

    }
}
