using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        public void LoadActiveOrders(Client client);
        public void LoadHotelRooms(Client client);
        public Client FindByPhoneNumber(string number);
    }
}
