using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IHotelRoomsAdminService: IDisposable
    {
        public int RoomsQuantity { get; }
        public IEnumerable<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1, int pageSize = 3);
        public void AddRoom(HotelRoomDTO room);
        public void EditRoom(HotelRoomDTO room);
        public void DeleteRoom(int deleteRoomId);
        public HotelRoomDTO FindRoom(int hotelRoomId);
    }
}
