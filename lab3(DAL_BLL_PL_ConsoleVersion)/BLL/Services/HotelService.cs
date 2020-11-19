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
        private IUnitOfWork UnitoOfWork { get; }
        private IMapper mapper { get; }
        public HotelService(IUnitOfWork unitOfWork)
        {
            UnitoOfWork = unitOfWork;
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
            var rooms = UnitoOfWork.HotelRooms.Find(p => (filter.TypeComfort == 0 || p.TypeComfort.Comfort == comfort) &&
                                                      (filter.TypeSize == 0 || p.TypeSize.Size == size) &&
                                                      p.ActiveOrders.All(t => t.ChecknInDate > filter.CheckInDate || t.CheckOutDate <= filter.CheckInDate), false);

            List<FreeHotelRoomDTO> result = new List<FreeHotelRoomDTO>();
            if (!rooms.Any())  // rooms.Count() не гуд, потому что будет перебирать всю колекцию
                return result;         

            foreach(var room in rooms)
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
        private Client TryFindClient(ClientDTO client)
        {
            if (client is null)
                throw new ArgumentNullException("client");

            return UnitoOfWork.Clients.Find(p => p.FirstName == client.FirstName && p.LastName == client.LastName
                                              && p.PhoneNumber == client.PhoneNumber)
                                   .FirstOrDefault();
        }
        public void AddActiveOrder(ActiveOrderDTO _order)
        {
            if (_order is null)
                throw new ArgumentNullException("_order");

            ActiveOrder order = mapper.Map<ActiveOrder>(_order);
            Client client = TryFindClient(_order.Client);
            if (!(client is null))
                order.Client = client;
            UnitoOfWork.ActiveOrders.Create(order); 
            UnitoOfWork.Save();
        }
        public IEnumerable<ActiveOrderDTO> FindActiveOrdersClient(ClientDTO _client)
        {
            if (_client is null)
                throw new ArgumentNullException("_client");

            Client client = TryFindClient(_client);
            if (!(client is null))
                return mapper.Map<List<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(client.ActiveOrders);
            return new List<ActiveOrderDTO>();
        }
        public void UpdateActiveOrder(ActiveOrderDTO _order)
        {
            if (_order is null)
                throw new ArgumentNullException("_order");

            ActiveOrder order = UnitoOfWork.ActiveOrders.Read(_order.ActiveOrderId);
            if (!(order is null))
            {
                mapper.Map<ActiveOrderDTO, ActiveOrder>(_order, order);
                UnitoOfWork.ActiveOrders.Update(order);
                UnitoOfWork.Save();
            }
        }
        public void Dispose()
        {
            UnitoOfWork.Dispose();
        }
    }
}
