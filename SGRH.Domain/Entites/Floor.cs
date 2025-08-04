using SGRH._Domain.Base;

namespace SGRH._Domain.Entites
{
    public class Floor : BaseEntity
    {
        public int FloorNumber { get; set; }

        public Floor(int entityFloorId, int floorNumber)
        {
            FloorNumber = floorNumber;

        }


        public Floor() : base() { }
    }
}