using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class HotelRoomDTO
    {
        public int HotelRoomId { get; set; }
        public string Number { get; set; }
        public decimal PricePerDay { get; set; }
        public TypeSizeEnumDTO TypeSize { get; set; }
        public TypeComfortEnumDTO TypeComfort { get; set; }
    }
}
