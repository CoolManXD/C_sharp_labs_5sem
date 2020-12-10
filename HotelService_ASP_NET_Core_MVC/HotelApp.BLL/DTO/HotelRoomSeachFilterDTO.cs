using System;

namespace HotelApp.BLL.DTO
{
    public class HotelRoomSeachFilterDTO
    {
        public TypeSizeEnumDTO TypeSize { get; set; }
        public TypeComfortEnumDTO TypeComfort { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
   
}
