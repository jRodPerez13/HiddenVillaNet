using Models;

namespace BussinesLogic.Repository.IRepository;

public interface IHotelImagesRepository
{
    public Task<int> CreateHotelRoomImage(HotelRoomImageDTO image);
    public Task<int> DeleteHotelRoomImageByImageId(int imageId);
    public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
    public Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl);

    public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
}
