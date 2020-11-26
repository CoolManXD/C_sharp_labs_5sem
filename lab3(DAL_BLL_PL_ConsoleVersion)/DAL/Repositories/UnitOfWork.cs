using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private HotelDbContext Context { get; }
        private IHotelRoomRepository hotelRoomRepository;
        private IClientRepository clientRepository;
        private IActiveOrderRepository activeRepository;
        public UnitOfWork(DbContextOptions<HotelDbContext> options)
        {
            Context = new HotelDbContext(options);
        }
        public IHotelRoomRepository HotelRooms
        {
            get
            {
                if (hotelRoomRepository == null)
                    hotelRoomRepository = new HotelRoomRepository(Context);
                return hotelRoomRepository;
            }
        }
        public IClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(Context);
                return clientRepository;
            }
        }
        public IActiveOrderRepository ActiveOrders
        {
            get
            {
                if (activeRepository == null)
                    activeRepository = new ActiveOrderRepository(Context);
                return activeRepository;
            }
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
