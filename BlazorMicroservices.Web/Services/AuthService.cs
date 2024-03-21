using BlazorMicroservices.Web.Services.IServices;
using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.AppStates;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlazorMicroservices.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly HttpClient _client;
        private readonly CurrentUserService _currentUserService;

        public AuthService(IBaseService baseService, ProtectedLocalStorage protectedLocalStorage, AuthenticationStateProvider authStateProvider, HttpClient client, CurrentUserService currentUserService)
        {
            _baseService = baseService;
            _authStateProvider = authStateProvider;
            _client = client;
            _protectedLocalStorage = protectedLocalStorage;
            _currentUserService = currentUserService;
        }
        public async Task<ResponseDto?> Register(RegisterRequestDto dto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "register",
                Data = dto
            });
        }
        //public async Task<ResponseDto?> Register(RegisterRequestDto dto)
        //{
        //    var content = JsonConvert.SerializeObject(dto);
        //    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        //    var response = await _client.PostAsync(SD.AuthApiBase + "register", bodyContent);
        //    var contentTemp = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<RegisterResponseDto>(contentTemp);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return new ResponseDto() { IsSuccessful = true };
        //    }
        //    else
        //    {
        //        return new ResponseDto() { IsSuccessful = false, Message = "Register failed!", Errors = result!.Errors.ToList() };
        //    }
        //}

        public async Task<ResponseDto> Login(LoginRequestDto dto)
        {
            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "login",
                Data = dto
            });

            if (response is not null)
            {
                if (response.IsSuccessful)
                {
                    var loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));

                    if (loginResponseDto is not null && loginResponseDto.IsSuccessful)
                    {
                        await _protectedLocalStorage.SetAsync(SD.JwtToken, loginResponseDto.Token!);
                        await _protectedLocalStorage.SetAsync(SD.UserDetails, loginResponseDto.UserDto!);

                        _currentUserService.User = loginResponseDto.UserDto;

                        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponseDto.Token);

                        ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(loginResponseDto.Token!);

                        return new ResponseDto() { IsSuccessful = true };
                    }
                }
                else
                {
                    return new ResponseDto { IsSuccessful = false, Message = response.Errors.FirstOrDefault()!, Errors = response.Errors };
                }
            }

            return new ResponseDto { IsSuccessful = false, Message = "Login failed!", Errors = new List<string> { "Login failed!", "No response!" } };
        }
        //public async Task<ResponseDto> Login(LoginRequestDto dto)
        //{
        //    var content = JsonConvert.SerializeObject(dto);
        //    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        //    var response = await _client.PostAsync(SD.AuthApiBase + "login", bodyContent);
        //    var contentTemp = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<ResponseDto>(contentTemp);

        //    var loginResult = JsonConvert.DeserializeObject<LoginResponseDto>(result.Result.ToString());

        //    if (response.IsSuccessStatusCode)
        //    {
        //        await _protectedLocalStorage.SetAsync(SD.JwtToken, loginResult.Token);
        //        await _protectedLocalStorage.SetAsync(SD.UserDetails, loginResult.UserDto);

        //        ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(loginResult.Token);

        //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

        //        return new ResponseDto() { IsSuccessful = true };
        //    }
        //    else
        //    {
        //        return new ResponseDto() { IsSuccessful = false, Message = "Login failed!" };
        //    }
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
