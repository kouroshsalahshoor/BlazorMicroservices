using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components;

namespace BlazorMicroservices.Web.Components.Auth
{
    public partial class Register
    {
        [Inject]
        public IAuthenticationService? _authSerivce { get; set; }
        [Inject]
        public NavigationManager? _navigationManager { get; set; }


        private RegisterRequestDto _model = new();
        private bool _isProcessing { get; set; } = false;
        private List<string> _errors { get; set; } = new();

        private async Task onValidSubmit()
        {
            _errors.Clear();
            _isProcessing = true;

            var result = await _authSerivce!.Register(_model);
            if (result.IsSuccessful)
            {
                //regiration is successful
                _navigationManager!.NavigateTo("/login");
            }
            else
            {
                //failure
                _errors = result.Errors.ToList();
                //_errors.Add(result.Message);
            }
            _isProcessing = false;
        }
    }
}