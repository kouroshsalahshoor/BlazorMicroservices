using BlazorMicroservices.Services.AuthApi.Models.Dtos;

namespace BlazorMicroservices.Services.AuthApi.Services.IServices
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string username, string roleName);
    }
}
