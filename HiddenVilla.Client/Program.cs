using Blazored.LocalStorage;
using HiddenVilla.Client;
using HiddenVilla.Client.Service;
using HiddenVilla.Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();
builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

await builder.Build().RunAsync();
