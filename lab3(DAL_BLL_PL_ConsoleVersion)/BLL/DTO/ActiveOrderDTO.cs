using System;

namespace BLL.DTO
{
    public class ActiveOrderDTO
    {
        public ClientDTO Client { get; set; }
        public int HotelRoomId { get; set; }
        public PaymentStateEnumDTO PaymentState { get; set; }
        public float Discount { get; set; }
        public DateTime ChecknInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DateRegistration { get; set; }
        
    }

    public enum PaymentStateEnumDTO : byte
    {
        Paid = 1,
        Booked
    }
}
