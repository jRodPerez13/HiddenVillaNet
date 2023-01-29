using Models;

namespace BussinesLogic.Repository.IRepository;

public interface IHotelAmenityRepository
{
    public Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenity);
    public Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenity);
    public Task<HotelAmenityDTO> GetHotelAmenity(int amenityId);
    public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities();
    public Task<int> DeleteHotelAmenity(int amenityId);
    public Task<HotelAmenityDTO> IsSameNameAmenityAlreadyExists(string name);
}
