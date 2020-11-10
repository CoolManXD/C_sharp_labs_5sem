using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class HotelRoomBL
    {
        public int HotelRoomId { get; set; }
        public string Number { get; set; }
        public decimal PricePerDay { get; set; }
        public TypeSizeEnumBLL TypeSize { get; set; }
        public TypeComfortEnumBLL TypeComfort { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
    }
    public enum TypeSizeEnumBLL : byte
    {
        SGL = 1,
        DBL,
        DBL_TWN,
        TRPL,
        DBL_EXB,
        TRPL_EXB
    }
    public enum TypeComfortEnumBLL : byte
    {
        Standart = 1,
        Suite,
        De_Luxe,
        Duplex,
        Family_Room,
        Honeymoon_Room
    }
}
