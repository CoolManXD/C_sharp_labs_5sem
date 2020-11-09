using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ActiveOrder
    {
        public int ActiveOrderId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public PaymentStateEnum PaymentState { get; set; }
        public float Discount { get; set; }
        public DateTime ChecknInDate { get; set; }
        public int Duration { get; set; }
        public DateTime DateRegistration { get; set; }
        
    }

    public enum PaymentStateEnum: byte
    {
        Paid = 1,
        Booked
    }
}
