

namespace SGRH._Domain.Entities
{
    public class Room
    {
        public int Id { get; private set; }
        public string RoomStatus { get; private set; }
        public int RoomNumber { get; private set; }
        public int CategoryId { get; private set; }
        public int FloorId { get; private set; }

        public Room(int id, string roomStatus, int roomNumber, int categoryId, int floorId)
        {
            Id = id;
            RoomStatus = roomStatus;
            RoomNumber = roomNumber;
            CategoryId = categoryId;
            FloorId = floorId;
        }
    }
}
