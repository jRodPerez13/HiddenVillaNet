﻿using Blazored.LocalStorage;
using CommonFiles;
using HiddenVilla.Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HiddenVilla.Client.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
    {
        _httpClient = client;
        _localStorage = localStorage;
    }
    public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
    {
        var content = JsonConvert.SerializeObject(userFromAuthentication);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/account/signin", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
            await _localStorage.SetItemAsync(SD.Local_UserDetails, result.userDTO);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthenticationResponseDTO { IsAuthSuccessful = true };
        }
        else
        {
            return result;
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(SD.Local_Token);
        await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration)
    {
        var content = JsonConvert.SerializeObject(userForRegistration);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/account/signup", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);
        if (response.IsSuccessStatusCode)
        {
            return new RegistrationResponseDTO
            {
                IsRegistrationSuccessful = true
            };
        }
        else
        {
            return result;
        }
    }
}
