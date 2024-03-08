using BlazorMicroservices.Services.AuthApi.Models;

namespace BlazorMicroservices.Services.AuthApi.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
