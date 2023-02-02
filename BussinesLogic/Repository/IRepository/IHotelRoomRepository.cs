using Models;

namespace BussinesLogic.Repository.IRepository;

public interface IHotelRoomRepository
{
    Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
    Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
    Task<HotelRoomDTO> GetHotelRoom(int roomId, string? checkInDate = null, string? checkOutDate = null);
    Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string? checkInDate = null, string? checkOutDate = null);
    Task<HotelRoomDTO> IsRoomUnique(string Name, int roomId = 0);
    Task<int> DeleteHotelRoom(int roomId);
}
