namespace SGRH.Web.Models.Base
{
    public abstract class RoomBase
    {


        public int roomId { get; set; }
        public string numeroHabitacion { get; set; }
        public string type { get; set; }
        public int floorId { get; set; }
        public double price { get; set; }
        public string status { get; set; }

        public RoomBase(int roomId, string numeroHabitacion, string type, int floorId, double price, string status)
        {
            this.roomId = roomId;
            this.numeroHabitacion = numeroHabitacion;
            this.type = type;
            this.floorId = floorId;
            this.price = price;
            this.status = status;
        }
    }

  
}
