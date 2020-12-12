using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelApp.BLL.Services
{
    public class ClientAdminService: IClientAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public ClientAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<ClientDTO> FindClients()
        {
            IEnumerable<Client> hotels = UnitOfWork.Clients.FindAll(false);
            return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(hotels);
        }
        public ClientDTO FindClient(int clientId)
        {
            Client client = UnitOfWork.Clients.FindById(clientId);
            if (!(client is null))
            {
                UnitOfWork.Clients.LoadActiveOrders(client);
                return Mapper.Map<ClientDTO>(client);
            }               
            return null;
        }
        public bool InsertClient(ClientDTO client)
        {
            if (client is null)
                throw new ArgumentNullException("client");
            Client newClient = Mapper.Map<Client>(client);
            UnitOfWork.Clients.Insert(newClient);
            UnitOfWork.Save();
            return true;
        }
        public bool UpdateClient(ClientDTO client)
        {
            if (client is null)
                throw new ArgumentNullException("client");
            if (!UnitOfWork.Clients.CheckAvailability(client.ClientId))
                return false;
            Client editClient = Mapper.Map<Client>(client);
            UnitOfWork.Clients.Update(editClient);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteClient(int deleteClientId)
        {
            if (!UnitOfWork.Clients.CheckAvailability(deleteClientId))
                return false;
            UnitOfWork.Clients.Delete(deleteClientId);
            UnitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}


