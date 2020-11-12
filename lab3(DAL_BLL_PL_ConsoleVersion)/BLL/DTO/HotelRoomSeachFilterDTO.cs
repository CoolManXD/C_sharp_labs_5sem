using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class HotelRoomSeachFilterDTO
    {
        public TypeSizeEnumDTO TypeSize { get; set; }
        public TypeComfortEnumDTO TypeComfort { get; set; }
        public DateTime CheckInDate { get; set; }
    }
   
}
