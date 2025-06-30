using SGRH._Domain.Base;

namespace SGRH._Domain.Entities
{
    public class Floor : BaseEntity
    {
        public int FloorNumber { get; set; }

        public Floor(int entityFloorId, int floorNumber)
        {
            FloorNumber = floorNumber;
        }

        protected Floor() : base() { }
    }
}