using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApp.BLL.Services
{
    public class ClientOrderService: IClientOrderService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public ClientOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter)
        {
            if (filter is null)
                throw new ArgumentNullException("filter");

            TypeComfortEnum comfort = Mapper.Map<TypeComfortEnum>(filter.TypeComfort);
            TypeSizeEnum size = Mapper.Map<TypeSizeEnum>(filter.TypeSize);
            var rooms = UnitOfWork.HotelRooms.FindFreeRooms(size, comfort, filter.CheckInDate, filter.CheckOutDate);


            List<FreeHotelRoomDTO> result = new List<FreeHotelRoomDTO>();
            if (!rooms.Any())  // rooms.Count() не гуд, потому что будет перебирать всю колекцию
                return result;

            if (!(filter.CheckOutDate is null))
            {
                result = Mapper.Map<IEnumerable<HotelRoom>, List<FreeHotelRoomDTO>>(rooms);
                foreach (var room in result)
                {
                    room.CheckInDate = filter.CheckInDate;
                    room.MaxCheckOutDate = filter.CheckOutDate;
                }
                return result;
            }
                               
            foreach(var room in rooms) // search for a period of time the room is free
            {
                DateTime? minDate = null;
                foreach(var date in room.ActiveOrders)
                {
                    if (date.CheckInDate > filter.CheckInDate && (minDate is null || minDate > date.CheckInDate))
                        minDate = date.CheckInDate;
                }
                var temp = Mapper.Map<HotelRoom, FreeHotelRoomDTO>(room);
                temp.CheckInDate = filter.CheckInDate;
                temp.MaxCheckOutDate = minDate;
                result.Add(temp);
            }
            return result;
        }
        public void AddClientActiveOrder(ActiveOrderDTO _order, ClientDTO _client)
        {
            if (_order is null)
                throw new ArgumentNullException("_order");
            if (_client is null)
                throw new ArgumentException("_client");

            ActiveOrder order = Mapper.Map<ActiveOrder>(_order);
            Client client = UnitOfWork.Clients.FindByPhoneNumber(_client.PhoneNumber);
            if (client is null)
            {
                client = Mapper.Map<Client>(_client);
                client.ActiveOrders.Add(order);
                UnitOfWork.Clients.Insert(client);                
            }    
            else
            {
                client.ActiveOrders.Add(order);
                UnitOfWork.Clients.Update(client);  
            }
            UnitOfWork.Save();
        }
        public IEnumerable<ActiveOrderDTO> FindClientActiveOrders(string phoneNumber, PaymentStateEnumDTO paymentState = default)
        {
            Client client = UnitOfWork.Clients.FindByPhoneNumber(phoneNumber);
            if (!(client is null))
            {
                // PaymentStateEnum state = Mapper.Map<PaymentStateEnum>(paymentState);
                UnitOfWork.Clients.LoadActiveOrders(client);
                return Mapper.Map<List<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(client.ActiveOrders);
            }               
            return new List<ActiveOrderDTO>();
        }        
        public void ConfirmPayment(int activeOrderId)
        {
            ActiveOrder order = UnitOfWork.ActiveOrders.FindById(activeOrderId);
            if (!(order is null))
            {
                order.PaymentState = PaymentStateEnum.P;
                UnitOfWork.ActiveOrders.Update(order);
                UnitOfWork.Save();
            }
        }
        //public void UpdateActiveOrder(ActiveOrderDTO _order)
        //{
        //    if (_order is null)
        //        throw new ArgumentNullException("_order");

        //    ActiveOrder order = UnitOfWork.ActiveOrders.FindById(_order.ActiveOrderId);
        //    if (!(order is null))
        //    {
        //        Mapper.Map<ActiveOrderDTO, ActiveOrder>(_order, order);
        //        UnitOfWork.ActiveOrders.Update(order);
        //        UnitOfWork.Save();
        //    }
        //}
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
