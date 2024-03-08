using BlazorMicroservices.Services.AuthApi.Models.Dto;

namespace BlazorMicroservices.Services.AuthApi.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string username, string roleName);
    }
}
