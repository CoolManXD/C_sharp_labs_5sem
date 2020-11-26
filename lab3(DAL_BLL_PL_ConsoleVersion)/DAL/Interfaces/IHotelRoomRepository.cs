using DAL.Entities;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IHotelRoomRepository: IRepository<HotelRoom>
    {
        public IEnumerable<HotelRoom> FindFreeRooms(DateTime checkInDate, TypeSizeEnum size = 0, TypeComfortEnum comfort = 0);
        public void LoadActiveOrders(HotelRoom room);
        public void LoadClients(HotelRoom room);
    }
}
