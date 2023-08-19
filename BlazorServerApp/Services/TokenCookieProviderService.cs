using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public class TokenCookieProviderService : ITokenProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenCookieProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(ApplicationConstants.AuthCookieName);
        }

        public async Task<string?> GetToken()
        {
            string? token = null;
            var hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(
                ApplicationConstants.AuthCookieName, out token);
            return hasToken is true ? token : null;
        }

        public async Task SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(ApplicationConstants.AuthCookieName, token);
        }
    }
}
