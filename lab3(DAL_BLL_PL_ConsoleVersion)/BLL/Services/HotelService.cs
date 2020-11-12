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
        private IUnitOfWork DataBase { get; }
        private IMapper mapper { get; }
        public HotelService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypeComfortEnumDTO, TypeComfortEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(default, default)).ReverseMap();
                cfg.CreateMap<TypeSizeEnumDTO, TypeSizeEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(default, default)).ReverseMap();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>()
                    .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
                    .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size));

                cfg.CreateMap<Client, ClientDTO>().ReverseMap();
                cfg.CreateMap<PaymentStateEnum, PaymentStateEnumDTO>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
                cfg.CreateMap<ActiveOrderDTO, ActiveOrder>();
            });
            mapper = config.CreateMapper();
        }
        public IEnumerable<HotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter)
        {
            TypeComfortEnum comfort = mapper.Map<TypeComfortEnum>(filter.TypeComfort);
            TypeSizeEnum size = mapper.Map<TypeSizeEnum>(filter.TypeSize);
            var rooms = DataBase.HotelRooms.Find(p => (filter.TypeComfort == 0 || p.TypeComfort.Comfort == comfort) &&
                                                      (filter.TypeSize == 0 || p.TypeSize.Size == size) &&
                                                      p.ActiveOrders.All(t => t.ChecknInDate > filter.CheckInDate || t.CheckOutDate <= filter.CheckInDate));
            if (rooms.Count() == 0)
                return null;

            List<HotelRoomDTO> result = new List<HotelRoomDTO>();
            foreach(var room in rooms)
            {
                DateTime? minDate = null;
                foreach(var date in room.ActiveOrders)
                {
                    if (date.ChecknInDate > filter.CheckInDate && (minDate is null || minDate > date.ChecknInDate))
                        minDate = date.ChecknInDate;
                }
                //result.Add(new HotelRoomDTO
                //{
                //    HotelRoomId = room.HotelRoomId,
                //    Number = room.Number,
                //    PricePerDay = room.PricePerDay,
                //    TypeComfort = Enum.Parse<TypeComfortEnumDTO>(room.TypeComfort.Comfort.ToString()),
                //    TypeSize = Enum.Parse<TypeSizeEnumDTO>(room.TypeSize.Size.ToString()),
                //    CheckInDate = filter.CheckInDate,
                //    MaxCheckOutDate = minDate
                //});
                var temp = mapper.Map<HotelRoom, HotelRoomDTO>(room);
                temp.CheckInDate = filter.CheckInDate;
                temp.MaxCheckOutDate = minDate;
                result.Add(temp);
            }
            return result;
        }
        private Client TryFindClient(ClientDTO client)
        {
            return DataBase.Clients.Find(p => p.FirstName == client.FirstName && p.LastName == client.LastName
                                              && p.PhoneNumber == client.PhoneNumber)
                                   .FirstOrDefault();
        }
        public void AddActiveOrder(ActiveOrderDTO order)
        {
            //Client client = TryFindClient(order.Client);
            //if (client == null)
            //    client = new Client { FirstName = order.Client.FirstName, LastName = order.Client.LastName, PhoneNumber = order.Client.PhoneNumber };
            //ActiveOrder newOrder = new ActiveOrder()
            //{
            //    Client = client,
            //    HotelRoomId = order.HotelRoomId,
            //    PaymentState = Enum.Parse<PaymentStateEnum>(order.PaymentState.ToString()),
            //    Discount = order.Discount,
            //    ChecknInDate = order.ChecknInDate,
            //    CheckOutDate = order.CheckOutDate,
            //    DateRegistration = order.DateRegistration
            //};

            ActiveOrder activeOrder = mapper.Map<ActiveOrder>(order);
            Client client = TryFindClient(order.Client);
            if (!(client is null))
                activeOrder.Client = client;
            DataBase.ActiveOrders.Create(activeOrder); 
            DataBase.Save();
        }
        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
