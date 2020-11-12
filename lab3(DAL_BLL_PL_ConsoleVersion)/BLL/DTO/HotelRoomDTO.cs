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
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
    }
    public enum TypeSizeEnumDTO : byte
    {
        SGL = 1,
        DBL,
        DBL_TWN,
        TRPL,
        DBL_EXB,
        TRPL_EXB
    }
    public enum TypeComfortEnumDTO : byte
    {
        Standart = 1,
        Suite,
        De_Luxe,
        Duplex,
        Family_Room,
        Honeymoon_Room
    }
}
