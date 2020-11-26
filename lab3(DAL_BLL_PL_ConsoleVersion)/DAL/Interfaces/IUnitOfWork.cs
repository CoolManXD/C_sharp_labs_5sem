using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRoomRepository HotelRooms { get; }
        IClientRepository Clients { get; }
        IActiveOrderRepository ActiveOrders { get; }
        void Save();
    }
}
