using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private HotelDbContext db;
        public ClientRepository(HotelDbContext context)
        {
            db = context;
        }
        public IEnumerable<Client> ReadAll()
        {
            return db.Clients;
        }
        public Client Read(int id)
        {
            return db.Clients.Find(id);
        }
        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return db.Clients.Where(predicate);
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
        public void Delete(Client item)
        {
            db.Clients.Remove(item);
        }
    }
}
