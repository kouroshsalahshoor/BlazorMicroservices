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
        private readonly HttpClient _client;

        public AuthenticationService(IBaseService baseService, ProtectedLocalStorage protectedLocalStorage, AuthenticationStateProvider authStateProvider, HttpClient client)
        {
            _baseService = baseService;
            _authStateProvider = authStateProvider;
            _client = client;
            _protectedLocalStorage = protectedLocalStorage;
        }
        public async Task<ResponseDto?> Register(RegisterRequestDto dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(SD.AuthApiBase + "register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterResponseDto>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new ResponseDto() { IsSuccessful = true };
            }
            else
            {
                return new ResponseDto() { IsSuccessful = false, Message = "Register failed!", Errors = result!.Errors.ToList() };
            }
        }
        //public async Task<ResponseDto?> Register(RegisterRequestDto dto)
        //{
        //    return await _baseService.SendAsync(new RequestDto
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Url = SD.AuthApiBase + "register",
        //        Data = dto
        //    });
        //}
        public async Task<ResponseDto> Login(LoginRequestDto dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(SD.AuthApiBase + "login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(contentTemp);

            var loginResult = JsonConvert.DeserializeObject<LoginResponseDto>(result.Result.ToString());

            if (response.IsSuccessStatusCode)
            {
                await _protectedLocalStorage.SetAsync(SD.JwtToken, loginResult.Token);
                await _protectedLocalStorage.SetAsync(SD.UserDetails, loginResult.UserDto);
               
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(loginResult.Token);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

                return new ResponseDto() { IsSuccessful = true };
            }
            else
            {
                return new ResponseDto() { IsSuccessful = false, Message = "Login failed!" };
            }
        }
        //public async Task<ResponseDto> Login(LoginRequestDto dto)
        //{
        //    var response = await _baseService.SendAsync(new RequestDto
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Url = SD.AuthApiBase + "login",
        //        Data = dto
        //    });

        //    if (response is not null && response.IsSuccessful)
        //    {
        //        var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(response.Result.ToString());

        //        if (loginResponseDto is not null)
        //        {
        //            await _protectedLocalStorage.SetAsync(SD.JwtToken, loginResponseDto.Token);
        //            await _protectedLocalStorage.SetAsync(SD.UserDetails, loginResponseDto.User);
        //            ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(loginResponseDto.Token);
        //            return new ResponseDto() { IsSuccessful = true };
        //        }
        //    }

        //    return new ResponseDto() { IsSuccessful = false, Message = "Login failed!" };
        //}

        public async Task Logout()
        {
            await _protectedLocalStorage.DeleteAsync(SD.JwtToken);
            await _protectedLocalStorage.DeleteAsync(SD.UserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;

        }
    }
}
