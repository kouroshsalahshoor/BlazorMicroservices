using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public interface IAuthService
    {
        Task<ResponseDto?> Login(LoginRequestDto loginRequest);
        Task<ResponseDto?> Register(RegisterDto registerDto);
        Task<ResponseDto?> AssignToRole(RegisterDto registerDto);
    }
}
