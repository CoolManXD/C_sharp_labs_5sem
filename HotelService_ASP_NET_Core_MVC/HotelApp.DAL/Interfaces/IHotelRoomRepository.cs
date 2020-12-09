using HotelApp.DAL.Entities;
using System;
using System.Collections.Generic;

namespace HotelApp.DAL.Interfaces
{
    public interface IHotelRoomRepository: IRepository<HotelRoom>
    {
        public IEnumerable<HotelRoom> FindFreeRooms(TypeSizeEnum size, TypeComfortEnum comfort, DateTime checkInDate, DateTime? checkOutDate = null);
        public IEnumerable<HotelRoom> GetRoomsPage(int pageIndex, int pageSize = 3);
        public void LoadActiveOrders(HotelRoom room);
        public void LoadClients(HotelRoom room);
    }
}
