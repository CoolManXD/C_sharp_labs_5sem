using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IHotelRoomsAdminService: IDisposable
    {
        public IEnumerable<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1);
        public void AddRoom(HotelRoomDTO room);
        public void EditRoom(HotelRoomDTO room);
        public void DeleteRoom(int deleteRoomId);
        public HotelRoomDTO FindRoom(int hotelRoomId);
    }
}
