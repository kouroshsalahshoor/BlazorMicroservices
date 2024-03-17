using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorMicroservices.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBaseService _baseService;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(IBaseService baseService, ProtectedLocalStorage protectedLocalStorage, AuthenticationStateProvider authStateProvider)
        {
            _baseService = baseService;
            _authStateProvider = authStateProvider;
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<ResponseDto?> RegisterUser(RegisterRequestDto dto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "register",
                Data = dto
            });
        }
        public async Task<ResponseDto> Login(LoginRequestDto dto)
        {
            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "login",
                Data = dto
            });

            if (response is not null && response.IsSuccessful)
            {
                var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(response.Result.ToString());

                await _protectedLocalStorage.SetAsync(SD.JwtToken, loginResponseDto.Token);
                await _protectedLocalStorage.SetAsync(SD.UserDetails, loginResponseDto.User);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(loginResponseDto.Token);
                return new ResponseDto() { IsSuccessful = true };
            }
            else
            {
                return new ResponseDto() { IsSuccessful = false, Message = "Login failed!" };
            }
        }

        public async Task Logout()
        {
            //await _protectedLocalStorage.DeleteAsync(SD.JwtToken);
            //await _protectedLocalStorage.DeleteAsync(SD.UserDetails);

            //((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            //_client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
