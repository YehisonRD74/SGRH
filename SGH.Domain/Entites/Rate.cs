namespace SGRH._Domain.Entities
{
    public class Rate
    {
        public int Id { get; private set; }
        public string Season { get; private set; }
        public decimal RatePrice { get; private set; }

        // Foreign Key
        public int CategoryId { get; private set; }
        
        public RoomCategory RoomCategory { get; private set; }
        public Rate(int id, string season, decimal ratePrice, int categoryId)
        {
            Id = id;
            Season = season;
            RatePrice = ratePrice;
            CategoryId = categoryId;
        }
    }
}