using System.Collections.Generic;

namespace HotelApp.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<ActiveOrderModel> ActiveOrders { get; set; } = new List<ActiveOrderModel>();
        //public List<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();
    }
}
