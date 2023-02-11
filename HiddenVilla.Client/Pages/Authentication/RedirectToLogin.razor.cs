using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HiddenVilla.Client.Pages.Authentication;
public partial class RedirectToLogin
{
    [Inject]
    private NavigationManager navigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    bool notAuthorized { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationState;
        var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);

        if (authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
        {
            navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
            return;
        }

        notAuthorized = true;
    }
}
