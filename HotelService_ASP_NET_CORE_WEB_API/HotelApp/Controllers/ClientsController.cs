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
    public class ClientsController : ControllerBase
    {
        private readonly IClientOrderService clientOrderService;
        private readonly IMapper mapper;
        public ClientsController(IClientOrderService clientOrderService, IMapper mapper)
        {
            this.clientOrderService = clientOrderService;
            this.mapper = mapper;
        }
        //[HttpGet("ClientOrders")]
        [HttpGet("Orders")]
        public IActionResult GetClientOrders([FromQuery] string phoneNumber)
        {
            IEnumerable<ActiveOrderDTO> orders = clientOrderService.FindClientActiveOrders(phoneNumber);
            return new ObjectResult(mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        }

        //[HttpPost("MakeOrder")]
        [HttpPost("Orders")]
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

        //[HttpPut("ConfirmPayment/{activeOrderId}")]
        [HttpPut("Orders/{activeOrderId}")]
        public IActionResult PutConfirmPayment(int activeOrderId)
        {
            clientOrderService.ConfirmPayment(activeOrderId);
            return Ok();
        }
    }
}
