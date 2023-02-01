using BussinesLogic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HiddenVilla.Api.Controllers;

[Route("api/[controller]")]
public class HotelRoomController : Controller
{
    private readonly IHotelRoomRepository _hotelRoomRepository;
    public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
    {
        _hotelRoomRepository = hotelRoomRepository;
    }

    [HttpGet]
    //[Authorize(Roles = CommonFiles.SD.Role_Admin)]
    public async Task<IActionResult> AllGetRooms()
    {
        var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
        return Ok(allRooms);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetHotelRoom(int? roomId)
    {
        if (roomId == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "Room Id is null",
                ErrorMessage = "Room Id is null",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value);
        if (roomDetails == null)
        {
            return NotFound(new ErrorModel()
            {
                Title = "Room Not Found",
                ErrorMessage = "Room Not Found",
                StatusCode = StatusCodes.Status404NotFound
            });
        }
        return Ok(roomDetails);
    }
}