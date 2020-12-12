using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IHotelRoomsAdminService: IDisposable
    {
        public int RoomsQuantity { get; }
        public IEnumerable<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1, int pageSize = 3, int hotelId = 0);
        public HotelRoomDTO FindRoom(int hotelRoomId);
        public bool InsertRoom(HotelRoomDTO room);
        public bool UpdateRoom(HotelRoomDTO room);
        public bool DeleteRoom(int deleteRoomId); 
    }
}
