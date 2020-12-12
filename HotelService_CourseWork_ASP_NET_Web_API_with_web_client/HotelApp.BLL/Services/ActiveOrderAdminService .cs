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
    public class ActiveOrderAdminService: IActiveOrderAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public ActiveOrderAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<ActiveOrderDTO> FindOrders()
        {
            IEnumerable<ActiveOrder> orders = UnitOfWork.ActiveOrders.FindAll(false);
            return Mapper.Map<IEnumerable<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(orders);
        }
        public ActiveOrderDTO FindOrder(int orderId)
        {
            ActiveOrder order = UnitOfWork.ActiveOrders.FindById(orderId);
            if (!(order is null))
            {
                //UnitOfWork.ActiveOrders.LoadActiveOrders(order);
                return Mapper.Map<ActiveOrderDTO>(order);
            }               
            return null;
        }
        public bool InsertOrder(ActiveOrderDTO order)
        {
            if (order is null)
                throw new ArgumentNullException("order");
            ActiveOrder newOrder = Mapper.Map<ActiveOrder>(order);
            UnitOfWork.ActiveOrders.Insert(newOrder);
            UnitOfWork.Save();
            return true;
        }
        public bool UpdateOrder(ActiveOrderDTO order)
        {
            if (order is null)
                throw new ArgumentNullException("order");
            if (!UnitOfWork.Clients.CheckAvailability(order.ActiveOrderId))
                return false;
            ActiveOrder editOrder = Mapper.Map<ActiveOrder>(order);
            UnitOfWork.ActiveOrders.Update(editOrder);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteOrder(int deleteOrderId)
        {
            if (!UnitOfWork.ActiveOrders.CheckAvailability(deleteOrderId))
                return false;
            UnitOfWork.ActiveOrders.Delete(deleteOrderId);
            UnitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}