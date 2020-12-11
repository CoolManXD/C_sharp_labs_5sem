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
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IHotelRoomsAdminService roomsAdminService;
        private readonly IMapper mapper;
        public RoomsController(IHotelRoomsAdminService roomsAdminService, IMapper mapper)
        {
            this.roomsAdminService = roomsAdminService;
            this.mapper = mapper;
        }

        [HttpGet("Page/{pageIndex?}")]
        public IActionResult GetRoomsPage(int pageIndex = 1)
        {
            var count = roomsAdminService.RoomsQuantity;
            PageViewModel pageViewModel = new PageViewModel(count, pageIndex, 3);
            IEnumerable<HotelRoomDTO> rooms = roomsAdminService.ShowRoomsPage(pageIndex, 3);
            RoomsPageViewModel roomsPageViewModel = new RoomsPageViewModel
            {
                HotelRooms = mapper.Map<IEnumerable<HotelRoomDTO>, IEnumerable<HotelRoomModel>>(rooms),
                PageViewModel = pageViewModel
            };
            return new ObjectResult(roomsPageViewModel);
        }

        [HttpGet("FreeRooms")]
        public IActionResult GetFreeRooms([FromQuery] HotelRoomSeachFilterModel filter, [FromServices] IClientOrderService clientOrderService)
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

        //[HttpPost("AddRoom")]
        [HttpPost]
        public IActionResult PostAddRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest();
            roomsAdminService.AddRoom(mapper.Map<HotelRoomDTO>(room));
            return Ok();
        }

        //[HttpGet("EditRoom/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetEditRoom(int id)
        {
            HotelRoomDTO room = roomsAdminService.FindRoom(id);
            if (room is null)
                return NotFound();
            return new ObjectResult(mapper.Map<HotelRoomModel>(room));
        }

        //[HttpPut("EditRoom")]
        [HttpPut]
        public IActionResult PutEditRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest();
            roomsAdminService.EditRoom(mapper.Map<HotelRoomDTO>(room));
            return Ok();
        }

        //[HttpDelete("DeleteRoom/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            if (roomsAdminService.FindRoom(id) is null)
                return NotFound();
            roomsAdminService.DeleteRoom(id);
            return Ok();
        }
    }
}
