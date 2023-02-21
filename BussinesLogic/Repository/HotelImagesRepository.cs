using AutoMapper;
using BussinesLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BussinesLogic.Repository;

public class HotelImagesRepository : IHotelImagesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
    {
        var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
        await _db.HotelRoomImages.AddAsync(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
    {
        var image = await _db.HotelRoomImages.FindAsync(imageId);
        _db.HotelRoomImages.Remove(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
    {
        var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync(x =>
        x.RoomImageUrl.ToLower() == imageUrl.ToLower());
        if (allImages == null) return 0;
        _db.HotelRoomImages.Remove(allImages);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
    {
        var imagesList = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
        _db.HotelRoomImages.RemoveRange(imagesList);
        return await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
    {
        return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());
    }
}
