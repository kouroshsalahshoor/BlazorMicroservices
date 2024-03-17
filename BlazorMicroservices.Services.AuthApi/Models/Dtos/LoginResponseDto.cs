﻿namespace BlazorMicroservices.Services.AuthApi.Models.Dtos
{
    public class LoginResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public UserDto? UserDto { get; set; }
    }
}
