using BlazorServerApp.Data;
using BlazorServerApp.Infrastructure;
using static BlazorServerApp.Infrastructure.ApplicationConstants;

namespace BlazorServerApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignToRole(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.POST,
                Url = ApplicationConstants.AuthApiUrl + "assigntorole",
                Data = registerDto
            }, false);
        }

        public async Task<ResponseDto?> Login(LoginRequestDto loginRequest)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.POST,
                Url = ApplicationConstants.AuthApiUrl + "login",
                Data = loginRequest
            }, false);
        }

        public async Task<ResponseDto?> Register(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypes.POST,
                Url = ApplicationConstants.AuthApiUrl + "register",
                Data = registerDto
            }, false);
        }
    }
}
