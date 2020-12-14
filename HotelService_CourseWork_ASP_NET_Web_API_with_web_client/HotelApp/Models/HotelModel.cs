
using System.Collections.Generic;

namespace HotelApp.Models
{
    public class HotelModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<HotelRoomModel> HotelRooms { get; set; } = new List<HotelRoomModel>();
    }
}
