using BussinesLogic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla.Api.Controllers;

[Route("api/[controller]")]
public class HotelAmenityController : Controller
{
    private readonly IHotelAmenityRepository _hotelAmenityRepository;


    public HotelAmenityController(IHotelAmenityRepository hotelAmenityRepository)
    {
        _hotelAmenityRepository = hotelAmenityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotelAmenities()
    {
        var allAmenities = await _hotelAmenityRepository.GetAllHotelAmenities();
        return Ok(allAmenities);
    }
}
