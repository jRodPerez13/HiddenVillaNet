using HiddenVilla.Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace HiddenVilla.Client.Pages.Authentication;
public partial class Logout
{
    [Inject]
    public IAuthenticationService? authenticationService { get; set; }
    [Inject]
    public NavigationManager? navigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await authenticationService.Logout();
        navigationManager.NavigateTo("/");
    }
}
