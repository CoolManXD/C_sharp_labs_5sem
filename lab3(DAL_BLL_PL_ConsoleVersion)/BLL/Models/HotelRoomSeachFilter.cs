using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class HotelRoomSeachFilter
    {
        public TypeSizeEnumBLL TypeSize { get; set; }
        public TypeComfortEnumBLL TypeComfort { get; set; }
        public DateTime CheckInDate { get; set; }
    }
   
}
