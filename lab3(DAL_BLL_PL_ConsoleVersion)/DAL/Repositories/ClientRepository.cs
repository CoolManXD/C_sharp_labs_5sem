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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(HotelDbContext context) : base(context)
        {
        }
        public void LoadActiveOrders(Client client)
        {
            context.Entry(client).Collection(p => p.ActiveOrders).Load();
        }
        public void LoadHotelRooms(Client client)
        {
            context.Entry(client).Collection(p => p.HotelRooms).Load();
        }
        public Client FindByPhoneNumber(string number)
        {
            return context.Clients.Where(p => p.PhoneNumber == number).SingleOrDefault();
        }
    }
}
