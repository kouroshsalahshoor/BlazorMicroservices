﻿using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.Auth;

namespace BlazorMicroservices.Web.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task Logout();
    }
}
