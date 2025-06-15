

namespace SGRH._Domain.Entities
{
    public class RoomCategory
    {
        public int Id { get; private set; }
        public string CategoryName { get; private set; }
        public string Description { get; private set; }
        public decimal BaseRate { get; private set; }

        public RoomCategory(int id, string categoryName, string description, decimal baseRate)
        {
            Id = id;
            CategoryName = categoryName;
            Description = description;
            BaseRate = baseRate;
        }
    }
}
