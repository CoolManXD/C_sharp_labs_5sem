using HotelApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Controllers
{
    public class ClientOrderController : Controller
    {
        private readonly IClientOrderService clientOrderService;
        public ClientOrderController(IClientOrderService clientOrderService)
        {
            this.clientOrderService = clientOrderService;
        }

    }
}
