using HiddenVilla.Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVilla.Client.Pages.Authentication;

public partial class Register
{
    private UserRequestDTO UserForRegistration = new UserRequestDTO();
    public bool IsProcessing { get; set; } = false;
    public bool ShowRegistrationErrors { get; set; }
    public IEnumerable<string> Errors { get; set; }

    [Inject]
    public IAuthenticationService? authenticationService { get; set; }
    [Inject]
    public NavigationManager? navigationManager { get; set; }
    private async Task RegisterUser()
    {

        ShowRegistrationErrors = false;
        IsProcessing = true;
        var result = await authenticationService.RegisterUser(UserForRegistration);
        if (result.IsRegistrationSuccessful)
        {
            IsProcessing = false;
            navigationManager.NavigateTo("/login");
        }
        else
        {
            IsProcessing = false;
            Errors = result.Errors;
            ShowRegistrationErrors = true;
        }
    }
}
