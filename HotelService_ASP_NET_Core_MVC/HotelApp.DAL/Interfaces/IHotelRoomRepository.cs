using HotelApp.DAL.Entities;
using System;
using System.Collections.Generic;

namespace HotelApp.DAL.Interfaces
{
    public interface IHotelRoomRepository: IRepository<HotelRoom>
    {
        public IEnumerable<HotelRoom> FindFreeRooms(DateTime checkInDate, TypeSizeEnum size = 0, TypeComfortEnum comfort = 0);
        public IEnumerable<HotelRoom> FindFreeRooms(DateTime checkInDate, DateTime checkOutDate, TypeSizeEnum size = 0, TypeComfortEnum comfort = 0);
        public IEnumerable<HotelRoom> GetRoomsPage(int pageIndex, int pageSize = 3);
        public void LoadActiveOrders(HotelRoom room);
        public void LoadClients(HotelRoom room);
    }
}
