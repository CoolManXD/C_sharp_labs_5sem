using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IReadOnlyRepository<HotelRoom> HotelRooms { get; }
        IRepository<Client> Clients { get; }
        IRepository<ActiveOrder> ActiveOrders { get; }
        void Save();
    }
}
