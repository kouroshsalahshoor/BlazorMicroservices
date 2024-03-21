using Microsoft.JSInterop;
namespace BlazorMicroservices.Web.Extentions
{
    public static class JsRuntimeExtentions
    {
        //For Success Message of an opration
        public static ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            return jSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        //for error message of an opration
        public static ValueTask ToastrError(this IJSRuntime jsSRuntime, string message)
        {
            return jsSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static ValueTask SwalSuccess(this IJSRuntime jSRuntime, string message)
        {
            return jSRuntime.InvokeVoidAsync("ShowSwal", "success", message);
        }
        public static ValueTask SwalError(this IJSRuntime jsSRuntime, string message)
        {
            return jsSRuntime.InvokeVoidAsync("ShowSwal", "error", message);
        }

    }
}