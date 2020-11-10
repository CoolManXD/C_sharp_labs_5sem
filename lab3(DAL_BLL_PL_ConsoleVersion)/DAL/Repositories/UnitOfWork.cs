using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private HotelDbContext DataBase { get; }
        private HotelRoomRepository hotelRoomRepository;
        private ClientRepository clientRepository;
        private ActiveOrderRepository activeOrderRepository;
        public UnitOfWork(DbContextOptions<HotelDbContext> options)
        {
            DataBase = new HotelDbContext(options);
        }
        public IReadOnlyRepository<HotelRoom> HotelRooms
        {
            get
            {
                if (hotelRoomRepository == null)
                    hotelRoomRepository = new HotelRoomRepository(DataBase);
                return hotelRoomRepository;
            }
        }
        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(DataBase);
                return clientRepository;
            }
        }
        public IRepository<ActiveOrder> ActiveOrders
        {
            get
            {
                if (activeOrderRepository == null)
                    activeOrderRepository = new ActiveOrderRepository(DataBase);
                return activeOrderRepository;
            }
        }

        public void Save()
        {
            DataBase.SaveChanges();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        
    }
}
