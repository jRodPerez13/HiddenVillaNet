using AutoMapper;
using BussinesLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BussinesLogic.Repository;

public class HotelAmenityRepository : IHotelAmenityRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenity)
    {
        HotelAmenity amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenity);
        amenity.CreatedBy = "";
        amenity.CreatedDate = DateTime.Now;
        var addHotelAmenity = await _db.HotelAmenities.AddAsync(amenity);
        await _db.SaveChangesAsync();
        return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addHotelAmenity.Entity);
    }

    public async Task<int> DeleteHotelAmenity(int amenityId)
    {
        HotelAmenity amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
        if (amenityDetails != null)
        {
            _db.HotelAmenities.Remove(amenityDetails);
        }
        return 0;
    }

    public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities()
    {
        try
        {
            IEnumerable<HotelAmenityDTO> hotelAmenityDTOs = _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(
                await _db.HotelAmenities.ToListAsync());
            return hotelAmenityDTOs;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityId)
    {
        try
        {
            HotelAmenityDTO hotelAmenity = _mapper.Map<HotelAmenity, HotelAmenityDTO>
                (await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityId));
            return hotelAmenity;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelAmenityDTO> IsSameNameAmenityAlreadyExists(string Name)
    {
        try
        {
            var amenityDetails = await _db.HotelAmenities.FirstOrDefaultAsync(
                x => x.Name.ToLower().Trim() == Name.ToLower().Trim());
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityDetails);
        }
        catch (Exception ex)
        {

        }
        return new HotelAmenityDTO();
    }

    public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenityDTO)
    {
        try
        {
            if (amenityId == hotelAmenityDTO.Id)
            {
                var amenitiesDetails = await _db.HotelAmenities.FindAsync(amenityId);
                var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO, amenitiesDetails);
                amenity.UpdatedBy = "";
                amenity.UpdatedDate = DateTime.Now;
                var updatedAmenity = _db.HotelAmenities.Update(amenity);
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedAmenity.Entity);
            }
            else //Invalid
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
