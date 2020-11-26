using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

// Обновления booked to paid (один ко многим Client to ActiveOrder). Добавить DI. Вынести Mapping в отдельный класс. Обновить PL; 

namespace BLL.Services
{
    public class HotelService: IHotelService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper mapper { get; }
        public HotelService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypeComfortEnumDTO, TypeComfortEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeComfortEnumDTO.Undefined, default)).ReverseMap();
                cfg.CreateMap<TypeSizeEnumDTO, TypeSizeEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeSizeEnumDTO.Undefined, default)).ReverseMap();
                cfg.CreateMap<HotelRoom, FreeHotelRoomDTO>()
                    .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
                    .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size));
                cfg.CreateMap<HotelRoom, HotelRoomDTO>()
                    .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
                    .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size)).ReverseMap();

                cfg.CreateMap<Client, ClientDTO>().ReverseMap();
                cfg.CreateMap<PaymentStateEnum, PaymentStateEnumDTO>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
                cfg.CreateMap<ActiveOrderDTO, ActiveOrder>().ReverseMap();
            });
            mapper = config.CreateMapper();
        }
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter)
        {
            if (filter is null)
                throw new ArgumentNullException("filter");

            TypeComfortEnum comfort = mapper.Map<TypeComfortEnum>(filter.TypeComfort);
            TypeSizeEnum size = mapper.Map<TypeSizeEnum>(filter.TypeSize);
            var rooms = UnitOfWork.HotelRooms.FindFreeRooms(filter.CheckInDate, size, comfort);

            List<FreeHotelRoomDTO> result = new List<FreeHotelRoomDTO>();
            if (!rooms.Any())  // rooms.Count() не гуд, потому что будет перебирать всю колекцию
                return result;         

            foreach(var room in rooms) // search for a period of time the room is free
            {
                DateTime? minDate = null;
                foreach(var date in room.ActiveOrders)
                {
                    if (date.ChecknInDate > filter.CheckInDate && (minDate is null || minDate > date.ChecknInDate))
                        minDate = date.ChecknInDate;
                }
                var temp = mapper.Map<HotelRoom, FreeHotelRoomDTO>(room);
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

            ActiveOrder order = mapper.Map<ActiveOrder>(_order);
            Client client = UnitOfWork.Clients.FindByPhoneNumber(_client.PhoneNumber);
            if (client is null)
            {
                client = mapper.Map<Client>(_client);
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
        public IEnumerable<ActiveOrderDTO> FindClientActiveOrders(string phoneNumber)
        {
            //if (_client is null)
            //    throw new ArgumentNullException("_client");

            Client client = UnitOfWork.Clients.FindByPhoneNumber(phoneNumber);
            if (!(client is null))
            {
                UnitOfWork.Clients.LoadActiveOrders(client);
                return mapper.Map<List<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(client.ActiveOrders);
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
        //        mapper.Map<ActiveOrderDTO, ActiveOrder>(_order, order);
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
