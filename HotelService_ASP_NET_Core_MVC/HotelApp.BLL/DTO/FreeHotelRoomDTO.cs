using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.DTO
{
    public class FreeHotelRoomDTO: HotelRoomDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
    }

}
