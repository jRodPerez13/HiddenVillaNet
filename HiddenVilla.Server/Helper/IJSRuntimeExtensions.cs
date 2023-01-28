using Microsoft.JSInterop;
namespace HiddenVilla.Server.Helper;

public static class IJSRuntimeExtensions
{
    public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
    {
        await jSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }
    public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
    {
        await jSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }
}
