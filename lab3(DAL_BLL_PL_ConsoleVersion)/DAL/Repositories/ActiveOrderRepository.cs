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
    public class ActiveOrderRepository : Repository<ActiveOrder>, IActiveOrderRepository
    {
        public ActiveOrderRepository(HotelDbContext context) : base(context)
        {
        }
    }
}
