using Microsoft.JSInterop;

namespace HiddenVilla_Server.Helper;

public static class IJSRuntimeExtension
{
    public static async ValueTask ToastrSuccess(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }    
    
    public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }

    public static async ValueTask SwalSuccess(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("showSwal", "success", message);
    }
    
    public static async ValueTask SwalError(this IJSRuntime JSRuntime, string message)
    {
        await JSRuntime.InvokeVoidAsync("showSwal", "error", message);
    }
}