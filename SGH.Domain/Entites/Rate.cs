using SGRH._Domain.Entities;

namespace SGRH._Domain.Entites
{
    public class Rate
    {
        public int Id { get; private set; }
        public string Season { get; private set; }
        public decimal RatePrice { get; private set; }
        
        public RoomCategory RoomCategory { get; private set; }
        public Rate(int id, string season, decimal ratePrice, int roomcategoryId)
        {
            Id = id;
            Season = season;
            RatePrice = ratePrice;
            roomcategoryId = roomcategoryId;
        }
    }
}