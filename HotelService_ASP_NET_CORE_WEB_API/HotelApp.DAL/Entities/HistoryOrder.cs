using System;


namespace HotelApp.DAL.Entities
{
    public class HistoryOrder
    {
        public int ActiveOrderId { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public float Discount { get; set; }
        public DateTime ChecknInDate { get; set; }
        public int Duration { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
