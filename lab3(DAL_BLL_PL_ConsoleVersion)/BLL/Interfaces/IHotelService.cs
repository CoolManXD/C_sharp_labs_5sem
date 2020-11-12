using BLL.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IHotelService: IDisposable
    {
        public IEnumerable<HotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter);
        public void AddActiveOrder(ActiveOrderDTO order);
    }
}
