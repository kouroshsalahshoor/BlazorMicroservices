using BlazorServerApp.Infrastructure;

namespace BlazorServerApp.Services
{
    public class TokenProviderService2 : ITokenProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProviderService2(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(ApplicationConstants.AuthCookieName);
        }

        public string? GetToken()
        {
            string? token = null;
            var hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(
                ApplicationConstants.AuthCookieName, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(ApplicationConstants.AuthCookieName, token);
        }
    }
}
