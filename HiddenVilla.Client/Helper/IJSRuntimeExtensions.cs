using Microsoft.JSInterop;

namespace HiddenVilla.Client.Helper;
public static class IJSRuntimeExtensions
{
    public static async ValueTask ToastrSuccess(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }
    public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }
}