﻿using HiddenVilla.Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace HiddenVilla.Client.Service;

public class RoomOrderDetailsService : IRoomOrderDetailsService
{
    private readonly HttpClient _client;

    public RoomOrderDetailsService(HttpClient client)
    {
        _client = client;
    }
    public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
    {
        throw new NotImplementedException();
    }

    public async Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
    {
        details.UserId = "dummy user";
        var content = JsonConvert.SerializeObject(details);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/roomorder/create", bodyContent);

        //string res = response.Content.ReadAsStringAsync().Result;
        if (response.IsSuccessStatusCode)
        {
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoomOrderDetailsDTO>(contentTemp);
            return result;
        }
        else
        {
            var contentTemp = await response.Content.ReadAsStringAsync();
            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
            throw new Exception(errorModel.ErrorMessage);
        }
    }
}
