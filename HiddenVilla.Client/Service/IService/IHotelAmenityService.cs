using Models;

namespace HiddenVilla.Client.Service.IService;

public interface IHotelAmenityService
{
    public Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities();
}
