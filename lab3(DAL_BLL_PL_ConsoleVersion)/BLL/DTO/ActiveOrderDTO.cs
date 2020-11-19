using System;

namespace BLL.DTO
{
    public class ActiveOrderDTO
    {
        public int ActiveOrderId { get; set; }
        public int ClientId { get; set; }
        public ClientDTO Client { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoomDTO HotelRoom { get; set; }
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
