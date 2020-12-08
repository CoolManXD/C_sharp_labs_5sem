using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Services
{
    public class HotelRoomsAdminService: IHotelRoomsAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public HotelRoomsAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1)
        {
            IEnumerable<HotelRoom> rooms = UnitOfWork.HotelRooms.GetRoomsPage(pageIndex);
            return Mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(rooms);
        }
        public HotelRoomDTO FindRoom(int hotelRoomId)
        {
            HotelRoom room = UnitOfWork.HotelRooms.FindById(hotelRoomId, false);
            if (!(room is null))
                return Mapper.Map<HotelRoomDTO>(room);
            return null;
        }
        public void AddRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException("room");
            HotelRoom newRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Insert(newRoom);
            UnitOfWork.Save();
        }
        public void EditRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException("room");
            HotelRoom editRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Update(editRoom);
            UnitOfWork.Save();
        }
        public void DeleteRoom(int deleteRoomId)
        {
            UnitOfWork.HotelRooms.Delete(deleteRoomId);
            UnitOfWork.Save();
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
