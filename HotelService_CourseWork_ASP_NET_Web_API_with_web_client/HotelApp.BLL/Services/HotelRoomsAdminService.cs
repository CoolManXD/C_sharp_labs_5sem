using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelApp.BLL.Services
{
    public class HotelRoomsAdminService: IHotelRoomsAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public int RoomsQuantity
        {
            get
            {
                return UnitOfWork.HotelRooms.GetQuery().Count();
            }
        }
        public HotelRoomsAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1, int pageSize = 3, int hotelId = 0)
        {
            IEnumerable<HotelRoom> rooms = UnitOfWork.HotelRooms.GetRoomsPage(pageIndex, pageSize, hotelId);
            return Mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(rooms);
        }
        public HotelRoomDTO FindRoom(int hotelRoomId)
        {
            HotelRoom room = UnitOfWork.HotelRooms.FindById(hotelRoomId, false);
            if (!(room is null))
                return Mapper.Map<HotelRoomDTO>(room);
            return null;
        }
        public bool InsertRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException("room");
            HotelRoom newRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Insert(newRoom);
            UnitOfWork.Save();
            return true;
        }
        public bool UpdateRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException("room");
            if (!UnitOfWork.HotelRooms.CheckAvailability(room.HotelRoomId))
                return false;
            HotelRoom editRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Update(editRoom);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteRoom(int deleteRoomId)
        {
            if (!UnitOfWork.HotelRooms.CheckAvailability(deleteRoomId))
                return false;
            UnitOfWork.HotelRooms.Delete(deleteRoomId);
            UnitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
