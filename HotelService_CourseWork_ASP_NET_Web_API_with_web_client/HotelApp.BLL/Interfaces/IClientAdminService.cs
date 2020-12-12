using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IClientAdminService: IDisposable
    {
        public IEnumerable<ClientDTO> FindClients();
        public ClientDTO FindClient(int clientId);
        public bool InsertClient(ClientDTO client);
        public bool UpdateClient(ClientDTO client);
        public bool DeleteClient(int deleteClientId);
    }
}
