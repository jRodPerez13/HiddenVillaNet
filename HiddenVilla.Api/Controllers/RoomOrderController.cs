using BussinesLogic.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HiddenVilla.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoomOrderController : Controller
{
    private readonly IRoomOrderDetailsRepository _repository;

    public RoomOrderController(IRoomOrderDetailsRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Create(details);
            return Ok(result);
        }
        else
        {
            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while creating Room Details/ Booking"
            });
        }
    }
}