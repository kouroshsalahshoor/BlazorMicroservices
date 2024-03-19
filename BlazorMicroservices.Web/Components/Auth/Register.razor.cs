using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components;

namespace BlazorMicroservices.Web.Components.Auth
{
    public partial class Register
    {
        [Inject]
        public IAuthService? _authSerivce { get; set; }
        [Inject]
        public NavigationManager? _navigationManager { get; set; }


        private RegisterRequestDto _model = new();
        private bool _isProcessing { get; set; } = false;
        private List<string> _roles { get; set; } = new();
        private List<string> _errors { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            _roles.Add(SD.Role_Users);
            _roles.Add(SD.Role_Admins);
        }

        private async Task onValidSubmit()
        {
            _errors.Clear();
            _isProcessing = true;

            var result = await _authSerivce!.Register(_model);
            if (result is null)
            {
                _errors.Add("Error connecting auth service!");
            }
            else
            {
                if (result.IsSuccessful)
                {
                    //regiration is successful
                    _navigationManager!.NavigateTo("/login");
                }
                else
                {
                    _errors = result.Errors.ToList();
                }
            }
            
            _isProcessing = false;
        }
    }
}