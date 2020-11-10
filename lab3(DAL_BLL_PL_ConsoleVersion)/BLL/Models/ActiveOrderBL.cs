using System;

namespace BLL.Models
{
    public class ActiveOrderBL
    {
        public ClientBL Client { get; set; }
        public HotelRoomBL HotelRoom { get; set; }
        public PaymentStateEnumBL PaymentState { get; set; }
        public float Discount { get; set; }
        public DateTime ChecknInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DateRegistration { get; set; }
        
    }

    public enum PaymentStateEnumBL: byte
    {
        Paid = 1,
        Booked
    }
}
