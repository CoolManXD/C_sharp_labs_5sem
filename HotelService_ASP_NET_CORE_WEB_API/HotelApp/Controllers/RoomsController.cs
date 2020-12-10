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

        [HttpPost("AddRoom")]
        public IActionResult PostAddRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest();
            roomsAdminService.AddRoom(mapper.Map<HotelRoomDTO>(room));
            return Ok();
        }

        [HttpGet("EditRoom/{id}")]
        public IActionResult GetEditRoom(int id)
        {
            HotelRoomDTO room = roomsAdminService.FindRoom(id);
            if (room is null)
                return NotFound();
            return new ObjectResult(mapper.Map<HotelRoomModel>(room));
        }

        [HttpPut("EditRoom")]
        public IActionResult PutEditRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest();
            roomsAdminService.EditRoom(mapper.Map<HotelRoomDTO>(room));
            return Ok();
        }

        [HttpDelete("DeleteRoom/{id}")]
        public IActionResult DeleteRoom(int id)
        {
            if (roomsAdminService.FindRoom(id) is null)
                return NotFound();
            roomsAdminService.DeleteRoom(id);
            return Ok();
        }
    }
}
