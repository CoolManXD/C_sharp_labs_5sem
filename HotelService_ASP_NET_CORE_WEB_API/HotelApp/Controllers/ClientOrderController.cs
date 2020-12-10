using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientOrderController : ControllerBase
    {
        private readonly IClientOrderService clientOrderService;
        private readonly IMapper mapper;
        public ClientOrderController(IClientOrderService clientOrderService, IMapper mapper)
        {
            this.clientOrderService = clientOrderService;
            this.mapper = mapper;
        }
        [HttpGet("FreeRooms")]
        public IActionResult GetFreeRooms(HotelRoomSeachFilterModel filter)
        {
            if (filter is null)
                return BadRequest();
            if (filter.CheckOutDate != null && filter.CheckInDate >= filter.CheckOutDate)
                ModelState.AddModelError("", "CheckInDate can't be more or equal than CheckOutDate");
            if (filter.CheckInDate.Date < DateTime.Today)
                ModelState.AddModelError("", "CheckInDate can't be less than current date");
            if (ModelState.IsValid)
            {
                IEnumerable<FreeHotelRoomDTO> rooms = clientOrderService.SearchFreeRooms(mapper.Map<HotelRoomSeachFilterDTO>(filter));
                return new ObjectResult(mapper.Map<IEnumerable<FreeHotelRoomDTO>, IEnumerable<FreeHotelRoomModel>>(rooms));
            }
            return BadRequest();
        }

        [HttpPost("MakeOrder")]
        public IActionResult PostOrder(ClientOrderViewModel request)
        {
            if (request is null || request.client is null || request.order is null)
                return BadRequest();
            if (request.order.CheckOutDate != null && request.order.CheckInDate >= request.order.CheckOutDate)
                ModelState.AddModelError("", "CheckInDate can't be more or equal than CheckOutDate");
            if (!ModelState.IsValid)
                return BadRequest();

            ClientDTO client = mapper.Map<ClientDTO>(request.client);
            ActiveOrderDTO order = mapper.Map<ActiveOrderDTO>(request.order);
            clientOrderService.AddClientActiveOrder(order, client);
            return Ok();
        }

        [HttpGet("ClientOrders")]
        public IActionResult GetClientOrders(string phoneNumber)
        {
            IEnumerable<ActiveOrderDTO> orders = clientOrderService.FindClientActiveOrders(phoneNumber);            
            return new ObjectResult(mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        }

        [HttpPut("ConfirmPayment/{activeOrderId}")]
        public IActionResult PutConfirmPayment(int activeOrderId)
        {
            clientOrderService.ConfirmPayment(activeOrderId);
            return Ok();
        }
    }
}
