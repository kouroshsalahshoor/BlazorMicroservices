using BlazorMicroservices.Web.Extentions;
using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Web;

namespace BlazorMicroservices.Web.Components.Pages.Auth
{
    public partial class Login
    {
        [Inject] public IAuthService _authSerivce { get; set; }
        [Inject] public IJSRuntime _js { get; set; }
        [Inject] public NavigationManager _navigationManager { get; set; }

        private LoginRequestDto _model = new();
        private bool _loading { get; set; } = false;
        private List<string> _errors { get; set; } = new();
        private string _returnUrl { get; set; } = string.Empty;

        private async Task onValidSubmit()
        {
            _errors.Clear();
            _loading = true;
            var result = await _authSerivce.Login(_model);
            if (result is null)
            {
                _errors.Add("Error connecting auth service!");
            }
            else
            {
                //login is successful
                if (result.IsSuccessful)
                {
                    await _js.ToastrSuccess("Login Success");
                    await _js.SwalSuccess("Login Success");

                    var absoluteUri = new Uri(_navigationManager.Uri);
                    var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                    _returnUrl = queryParam["returnUrl"]!;
                    if (string.IsNullOrEmpty(_returnUrl))
                    {
                        _navigationManager.NavigateTo("/");
                    }
                    else
                    {
                        _navigationManager.NavigateTo("/" + _returnUrl);
                    }
                }
                else
                {
                    //_errors.Add(result!.Message);
                    _errors.AddRange(result.Errors.ToList());
                }
            }

            _loading = false;
        }
    }
}
