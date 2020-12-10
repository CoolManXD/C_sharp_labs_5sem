using System;
using HotelApp.DAL.Entities;

namespace HotelApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRoomRepository HotelRooms { get; }
        IClientRepository Clients { get; }
        IActiveOrderRepository ActiveOrders { get; }
        void Save();
    }
}
