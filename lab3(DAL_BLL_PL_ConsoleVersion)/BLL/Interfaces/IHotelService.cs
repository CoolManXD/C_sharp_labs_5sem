using BLL.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IHotelService: IDisposable
    {
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter);
        public void AddActiveOrder(ActiveOrderDTO order);
        public IEnumerable<ActiveOrderDTO> FindActiveOrdersClient(ClientDTO _client);
        public void UpdateActiveOrder(ActiveOrderDTO _order);
    }
}
