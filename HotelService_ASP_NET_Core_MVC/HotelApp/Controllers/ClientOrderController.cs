using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelApp.Controllers
{
    public class ClientOrderController : Controller
    {
        private readonly IClientOrderService clientOrderService;
        private readonly IMapper mapper;
        public ClientOrderController(IClientOrderService clientOrderService, IMapper mapper)
        {
            this.clientOrderService = clientOrderService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult SearchFreeRooms()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchFreeRooms(HotelRoomSeachFilterModel filter)
        {
            if (filter.CheckOutDate != null && filter.CheckInDate >= filter.CheckOutDate)
                ModelState.AddModelError("", "CheckInDate can't be more or equal than CheckOutDate");
            if (filter.CheckInDate.Date < DateTime.Today)
                ModelState.AddModelError("", "CheckInDate can't be less than current date");
            if (ModelState.IsValid)
            {
                IEnumerable<FreeHotelRoomDTO> rooms = clientOrderService.SearchFreeRooms(mapper.Map<HotelRoomSeachFilterDTO>(filter));
                return View("FreeRoomsPage", mapper.Map<IEnumerable<FreeHotelRoomDTO>, IEnumerable<FreeHotelRoomModel>>(rooms));
            }
            return View(filter);
        }
        [HttpGet]
        public IActionResult MakeOrder(ActiveOrderModel order)
        {
            return View(order);
        }
        [HttpPost]
        public IActionResult MakeOrder(ClientOrderViewModel request)
        {
            if (request.order.CheckOutDate != null && request.order.CheckInDate >= request.order.CheckOutDate)
                ModelState.AddModelError("", "CheckInDate can't be more or equal than CheckOutDate");
            if (!ModelState.IsValid)
                return View(request.order);

            ClientDTO client = mapper.Map<ClientDTO>(request.client);
            ActiveOrderDTO order = mapper.Map<ActiveOrderDTO>(request.order);
            clientOrderService.AddClientActiveOrder(order, client);
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public IActionResult SearchClientOrders()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchClientOrders(string phoneNumber)
        {
            IEnumerable<ActiveOrderDTO> orders = clientOrderService.FindClientActiveOrders(phoneNumber);
             
            return View("ClientOrdersPage", mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        }
        public IActionResult ConfirmPayment(int activeOrderId)
        {
            clientOrderService.ConfirmPayment(activeOrderId);
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
