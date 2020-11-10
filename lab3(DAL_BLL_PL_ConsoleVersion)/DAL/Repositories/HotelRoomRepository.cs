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
    public class HotelRoomRepository: IReadOnlyRepository<HotelRoom>
    {
        private HotelDbContext db;
        public HotelRoomRepository(HotelDbContext context)
        {
            db = context; 
        }
        public IEnumerable<HotelRoom> ReadAll()
        {
            //return db.HotelRooms.AsNoTracking().Include(p => p.TypeSize).Include(p => p.TypeComfort);
            return db.HotelRooms.Include(p => p.TypeSize).Include(p => p.TypeComfort).Include(p => p.ActiveOrders).AsNoTracking();
        }
        public HotelRoom Read(int id)
        {
            return db.HotelRooms.Where(p => p.HotelRoomId == id).Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).AsNoTracking().FirstOrDefault();
            //var temp = db.HotelRooms.AsNoTracking().FirstOrDefault(p => p.HotelRoomId == id);
            //if (temp != null)
            //{
            //    db.Entry(temp).Reference(p => p.TypeComfort).Load();
            //    db.Entry(temp).Reference(p => p.TypeSize).Load();
            //}   
            //return null;
        }
        public IEnumerable<HotelRoom> Find(Expression<Func<HotelRoom, bool>> predicate)
        {
            return db.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).Where(predicate).AsNoTracking();
        }
    }
}
