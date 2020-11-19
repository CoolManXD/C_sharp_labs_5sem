using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private HotelDbContext db;
        public ClientRepository(HotelDbContext context)
        {
            db = context;
        }
        public IEnumerable<Client> ReadAll(bool isTracked = true)
        {
            if (isTracked)
                return db.Clients;
            return db.Clients.AsNoTracking();
        }
        public Client Read(int id, bool isTracked = true)
        {
            if (isTracked)
                return db.Clients.Find(id);
            return db.Clients.Where(p => p.ClientId == id).AsNoTracking().FirstOrDefault();
        }
        public IEnumerable<Client> Find(Expression<Func<Client, bool>> predicate, bool isTracked = true)
        {
            if (isTracked)
                return db.Clients.Include(p => p.ActiveOrders).Where(predicate);
            return db.Clients.Include(p => p.ActiveOrders).Where(predicate).AsNoTracking();
        }
        public void Create(Client item)
        {
            db.Clients.Add(item);
        }
        public void Update(Client item)
        {
            db.Clients.Update(item);
        }
        public void Delete(int id)
        {
            var temp = db.Clients.Find(id);
            if (temp != null)
                db.Clients.Remove(temp);
        }
    }
}
