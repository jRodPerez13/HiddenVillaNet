using HiddenVilla.Client.Service.IService;
using Models;
using Newtonsoft.Json;

namespace HiddenVilla.Client.Service;

public class HotelAmenityService : IHotelAmenityService
{
    private readonly HttpClient _client;

    public HotelAmenityService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities()
    {
        var response = await _client.GetAsync($"api/hotelamenity");
        var content = await response.Content.ReadAsStringAsync();
        var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
        return rooms;
    }
}