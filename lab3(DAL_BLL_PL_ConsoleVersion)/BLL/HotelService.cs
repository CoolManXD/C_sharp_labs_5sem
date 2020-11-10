using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class HotelService: IDisposable
    {
        private IUnitOfWork DataBase { get; }
        public HotelService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }
        public IEnumerable<HotelRoomBL> SearchFreeRooms(HotelRoomSeachFilter filter)
        {
            TypeComfortEnum comfort = Enum.Parse<TypeComfortEnum>(filter.TypeComfort.ToString());
            TypeSizeEnum size = Enum.Parse<TypeSizeEnum>(filter.TypeSize.ToString());

            var rooms = DataBase.HotelRooms.Find(p => (filter.TypeComfort == 0 || p.TypeComfort.Comfort == comfort) &&
                                                      (filter.TypeSize == 0 || p.TypeSize.Size == size) &&
                                                      p.ActiveOrders.All(t => t.ChecknInDate > filter.CheckInDate || t.CheckOutDate <= filter.CheckInDate));
            if (rooms.Count() == 0)
                return null;

            List<HotelRoomBL> result = new List<HotelRoomBL>();
            foreach(var room in rooms)
            {
                if (room.ActiveOrders.Count() == 0)
                {
                    result.Add(new HotelRoomBL
                    {
                        HotelRoomId = room.HotelRoomId,
                        Number = room.Number,
                        PricePerDay = room.PricePerDay,
                        TypeComfort = Enum.Parse<TypeComfortEnumBLL>(room.TypeComfort.Comfort.ToString()),
                        TypeSize = Enum.Parse<TypeSizeEnumBLL>(room.TypeSize.Size.ToString()),
                        CheckInDate = filter.CheckInDate,
                        MaxCheckOutDate = null
                    });
                    continue;
                }
                DateTime? minDate = null;
                foreach(var date in room.ActiveOrders)
                {
                    if (date.ChecknInDate > filter.CheckInDate && (minDate == null || minDate > date.ChecknInDate))
                        minDate = date.ChecknInDate;
                }
                result.Add(new HotelRoomBL
                {
                    HotelRoomId = room.HotelRoomId,
                    Number = room.Number,
                    PricePerDay = room.PricePerDay,
                    TypeComfort = Enum.Parse<TypeComfortEnumBLL>(room.TypeComfort.Comfort.ToString()),
                    TypeSize = Enum.Parse<TypeSizeEnumBLL>(room.TypeSize.Size.ToString()),
                    CheckInDate = filter.CheckInDate,
                    MaxCheckOutDate = minDate
                });
            }
            return result;
        }
        private Client TryFindClient(ClientBL client)
        {
            return DataBase.Clients.Find(p => p.FirstName == client.FirstName && p.LastName == client.LastName
                                                  && p.PhoneNumber == client.PhoneNumber)
                                            .FirstOrDefault();
        }
        public void AddActiveOrder(ActiveOrderBL order)
        {
            Client client = TryFindClient(order.Client);
            if (client == null)
                client = new Client { FirstName = order.Client.FirstName, LastName = order.Client.LastName, PhoneNumber = order.Client.PhoneNumber };

            ActiveOrder newOrder = new ActiveOrder()
            {
                Client = client,
                HotelRoomId = order.HotelRoom.HotelRoomId,
                PaymentState = Enum.Parse<PaymentStateEnum>(order.PaymentState.ToString()),
                Discount = order.Discount,
                ChecknInDate = order.ChecknInDate,
                CheckOutDate = order.CheckOutDate,
                DateRegistration = order.DateRegistration
            };
            DataBase.ActiveOrders.Create(newOrder); 
            DataBase.Save();
        }
        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
