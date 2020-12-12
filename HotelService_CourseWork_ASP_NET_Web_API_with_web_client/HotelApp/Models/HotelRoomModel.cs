
namespace HotelApp.Models
{
    public class HotelRoomModel
    {
        public int HotelRoomId { get; set; }
        public string Number { get; set; }
        public decimal PricePerDay { get; set; }
        public TypeSizeEnumModel TypeSize { get; set; }
        public TypeComfortEnumModel TypeComfort { get; set; }
    }
}
