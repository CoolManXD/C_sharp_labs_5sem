using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IHotelRoomsAdminService roomsAdminService;
        public RoomsController(IHotelRoomsAdminService roomsAdminService)
        {
            this.roomsAdminService = roomsAdminService;
        }
        public IActionResult ShowRoomsPage(int pageIndex = 1)
        {
            ViewData["pageIndex"] = pageIndex;
            IEnumerable<HotelRoomDTO> rooms = roomsAdminService.ShowRoomsPage(pageIndex);
            return View(rooms);
        }
        public IActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRoom(HotelRoomDTO room)
        {
            roomsAdminService.AddRoom(room);
            return Redirect("~/Rooms/ShowRoomsPage");
        }
        public IActionResult EditPage(int id)
        {
            HotelRoomDTO room = roomsAdminService.FindRoom(id);
            //ViewBag.Room = room;
            return View(room);
        }
        [HttpPost]
        public IActionResult EditRoom(HotelRoomDTO room)
        {
            roomsAdminService.EditRoom(room);
            return Redirect("~/Rooms/ShowRoomsPage");
        }
        public IActionResult DeleteRoom(int id)
        {
            roomsAdminService.DeleteRoom(id);
            return Redirect("~/Rooms/ShowRoomsPage");
        }
        public IActionResult SearchFreeRoomsForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FreeRoomsPage(HotelRoomSeachFilterDTO filter, [FromServices] IClientOrderService service)
        {
            var rooms = service.SearchFreeRooms(filter);
            return View(rooms);
        }
    }
}
