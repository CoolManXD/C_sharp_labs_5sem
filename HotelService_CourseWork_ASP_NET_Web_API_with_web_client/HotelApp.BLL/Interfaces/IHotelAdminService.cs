using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IHotelAdminService: IDisposable
    {
        public IEnumerable<HotelDTO> FindHotels();
        public HotelDTO FindHotel(int hotelId);
        public bool InsertHotel(HotelDTO hotel);
        public bool UpdateHotel(HotelDTO hotel);
        public bool DeleteHotel(int deleteHotelId);
        public IEnumerable<ActiveOrderDTO> GetHotelOrderPeriod(int hotelId, DateTime start, DateTime end);
    }
}
