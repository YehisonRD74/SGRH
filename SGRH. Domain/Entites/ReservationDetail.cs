

namespace SGRH._Domain.Entities
{
    public class ReservationDetail
    {
        public int Id { get; private set; }
        public decimal NightPrice { get; private set; }
        public decimal Subtotal { get; private set; }
        public int ReservationId { get; private set; }
        public int RoomId { get; private set; }

        public ReservationDetail(int id, decimal nightPrice, decimal subtotal, int reservationId, int roomId)
        {
            Id = id;
            NightPrice = nightPrice;
            Subtotal = subtotal;
            ReservationId = reservationId;
            RoomId = roomId;
        }
    }
}
