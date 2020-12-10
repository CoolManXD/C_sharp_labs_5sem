using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IHotelRoomsAdminService roomsAdminService;
        private readonly IMapper mapper;
        public RoomsController(IHotelRoomsAdminService roomsAdminService, IMapper mapper)
        {
            this.roomsAdminService = roomsAdminService;
            this.mapper = mapper;
        }
        public IActionResult ShowRoomsPage(int pageIndex = 1)
        {
            ViewData["pageIndex"] = pageIndex;
            IEnumerable<HotelRoomDTO> rooms = roomsAdminService.ShowRoomsPage(pageIndex);
            return View(mapper.Map<IEnumerable<HotelRoomDTO>, IEnumerable<HotelRoomModel>>(rooms));
        }
        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRoom(HotelRoomModel room)
        {
            roomsAdminService.AddRoom(mapper.Map<HotelRoomDTO>(room));
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public IActionResult EditRoom(int id)
        {
            HotelRoomDTO room = roomsAdminService.FindRoom(id);
            return View(mapper.Map<HotelRoomModel>(room));
        }
        [HttpPost]
        public IActionResult EditRoom(HotelRoomModel room)
        {
            roomsAdminService.EditRoom(mapper.Map<HotelRoomDTO>(room));
            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult DeleteRoom(int id)
        {
            roomsAdminService.DeleteRoom(id);
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
