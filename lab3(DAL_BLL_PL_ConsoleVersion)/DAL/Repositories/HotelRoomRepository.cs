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
    public class HotelRoomRepository: IRepository<HotelRoom>
    {
        private HotelDbContext db;
        public HotelRoomRepository(HotelDbContext context)
        {
            db = context; 
        }
        public IEnumerable<HotelRoom> ReadAll(bool isTracked = true)
        {
            if (isTracked)
                return db.HotelRooms.Include(p => p.TypeSize).Include(p => p.TypeComfort).Include(p => p.ActiveOrders);
            return db.HotelRooms.Include(p => p.TypeSize).Include(p => p.TypeComfort).Include(p => p.ActiveOrders).AsNoTracking();
        }
        public HotelRoom Read(int id, bool isTracked = true)
        {
            if (isTracked)
                return db.HotelRooms.Where(p => p.HotelRoomId == id).Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).FirstOrDefault();
            return db.HotelRooms.Where(p => p.HotelRoomId == id).Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).AsNoTracking().FirstOrDefault();
            //var temp = db.HotelRooms.AsNoTracking().FirstOrDefault(p => p.HotelRoomId == id);
            //if (temp != null)
            //{
            //    db.Entry(temp).Reference(p => p.TypeComfort).Load();
            //    db.Entry(temp).Reference(p => p.TypeSize).Load();
            //}   
            //return null;
        }
        public IEnumerable<HotelRoom> Find(Expression<Func<HotelRoom, bool>> predicate, bool isTracked = true)
        {
            if (isTracked)
                return db.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).Where(predicate);
            return db.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders).Where(predicate).AsNoTracking();
        }
        public void Create(HotelRoom item)
        {
            db.HotelRooms.Add(item);
        }
        public void Update(HotelRoom item)
        {
            db.HotelRooms.Update(item);
        }
        public void Delete(int id)
        {
            var temp = db.HotelRooms.Find(id);
            if (temp != null)
                db.HotelRooms.Remove(temp);
        }
    }
}
