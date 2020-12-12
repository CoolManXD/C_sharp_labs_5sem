using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;

namespace HotelApp.BLL.Interfaces
{
    public interface IClientOrderService: IDisposable
    {
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter);
        public void AddClientActiveOrder(ActiveOrderDTO _order, ClientDTO _client);
        public IEnumerable<ActiveOrderDTO> FindClientActiveOrders(string phoneNumber, PaymentStateEnumDTO paymentState = default);
        public void ConfirmPayment(int activeOrderId);
    }
}
