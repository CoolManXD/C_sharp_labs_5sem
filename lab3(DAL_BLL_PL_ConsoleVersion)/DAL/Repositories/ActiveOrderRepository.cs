using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ActiveOrderRepository : IRepository<ActiveOrder>
    {
        private HotelDbContext db;
        public ActiveOrderRepository(HotelDbContext context)
        {
            db = context;
        }
        public IEnumerable<ActiveOrder> ReadAll(bool isTracked = true)
        {
            if (isTracked)
                return db.ActiveOrders;
            return db.ActiveOrders.AsNoTracking();
        }
        public ActiveOrder Read(int id, bool isTracked = true)
        {
            if (isTracked)
                return db.ActiveOrders.Find(id);
            return db.ActiveOrders.Where(p => p.ActiveOrderId == id).AsNoTracking().FirstOrDefault();
        }
        public IEnumerable<ActiveOrder> Find(Expression<Func<ActiveOrder, bool>> predicate, bool isTracked = true)
        {
            if (isTracked)
                return db.ActiveOrders.Where(predicate);
            return db.ActiveOrders.Where(predicate).AsNoTracking();
        }
        public void Create(ActiveOrder item)
        {
            db.ActiveOrders.Add(item);
        }
        public void Update(ActiveOrder item)
        {
            db.ActiveOrders.Update(item);
        }
        public void Delete(int id)
        {
            var temp = db.ActiveOrders.Find(id);
            if (temp != null)
                db.ActiveOrders.Remove(temp);
        }
    }
}
