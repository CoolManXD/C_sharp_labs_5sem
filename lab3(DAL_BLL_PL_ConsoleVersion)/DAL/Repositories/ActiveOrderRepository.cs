using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class ActiveOrderRepository : IRepository<ActiveOrder>
    {
        private HotelDbContext db;
        public ActiveOrderRepository(HotelDbContext context)
        {
            db = context;
        }
        public IEnumerable<ActiveOrder> ReadAll()
        {
            return db.ActiveOrders;
        }
        public ActiveOrder Read(int id)
        {
            return db.ActiveOrders.Find(id);
        }
        public IEnumerable<ActiveOrder> Find(Func<ActiveOrder, bool> predicate)
        {
            return db.ActiveOrders.Where(predicate);
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
        public void Delete(ActiveOrder item)
        {
            db.ActiveOrders.Remove(item);
        }
    }
}
