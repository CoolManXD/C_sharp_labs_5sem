using System;

namespace HotelApp.BLL.DTO
{
    public class ActiveOrderDTO
    {
        public int ActiveOrderId { get; set; }
        // public int ClientId { get; set; }
        // public ClientDTO Client { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoomDTO HotelRoom { get; set; }
        public PaymentStateEnumDTO PaymentState { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }

    public enum PaymentStateEnumDTO : byte
    {
        P = 1,
        B
    }
}
