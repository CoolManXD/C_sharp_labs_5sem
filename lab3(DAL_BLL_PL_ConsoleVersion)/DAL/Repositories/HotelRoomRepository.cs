using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class HotelRoomRepository: Repository<HotelRoom>, IHotelRoomRepository
    {
        public HotelRoomRepository(HotelDbContext context): base(context)
        {
        }
        public override IEnumerable<HotelRoom> FindAll(bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Include(p => p.TypeSize).Include(p => p.TypeComfort);
            if (!isTracked)
                set.AsNoTracking();
            return set.ToList();
        }
        public override HotelRoom FindById(int id, bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Where(p => p.HotelRoomId == id).Include(p => p.TypeComfort).Include(p => p.TypeSize);
            if (!isTracked)
                set.AsNoTracking();
            return set.SingleOrDefault();        
        }
        public override IEnumerable<HotelRoom> Find(Expression<Func<HotelRoom, bool>> predicate, bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Where(predicate);
            if (!isTracked)
                return set.AsNoTracking();
            return set.ToList();
        }
        public IEnumerable<HotelRoom> FindFreeRooms(DateTime checkInDate, TypeSizeEnum size = 0, TypeComfortEnum comfort = 0)
        {
            // если дата с прошлого, то сразу вернуть пустой список
            IQueryable<HotelRoom> rooms = context.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders);
            if (size != 0)
                rooms = rooms.Where(p => p.TypeSize.Size == size);
            if (comfort != 0)
                rooms = rooms.Where(p => p.TypeComfort.Comfort == comfort);
            return rooms.Where(p => p.ActiveOrders.All(t => t.ChecknInDate > checkInDate || t.CheckOutDate <= checkInDate)).AsNoTracking().ToList();

            //var rooms = UnitoOfWork.HotelRooms.Find(p => (filter.TypeComfort == 0 || p.TypeComfort.Comfort == comfort) &&
            //                                          (filter.TypeSize == 0 || p.TypeSize.Size == size) &&
            //                                          p.ActiveOrders.All(t => t.ChecknInDate > filter.CheckInDate || t.CheckOutDate <= filter.CheckInDate), false);
        }
        public void LoadActiveOrders(HotelRoom room)
        {
            context.Entry(room).Collection(p => p.ActiveOrders).Load();
        }
        public void LoadClients(HotelRoom room)
        {
            context.Entry(room).Collection(p => p.Clients).Load();
        }

        //на будущее, постраничный просмотр
        //public IEnumerable<HotelRoom> GetPage(int pageIndex, int pageSize = 10)
        //{
        //    return context.HotelRooms
        //        .OrderBy(p => p.Number)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}
    }
}
