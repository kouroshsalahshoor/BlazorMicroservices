using BlazorServerApp.Infrastructure;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorServerApp.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly ProtectedLocalStorage _protectedLocalStore;

        public TokenProviderService(ProtectedLocalStorage ProtectedLocalStore)
        {
            _protectedLocalStore = ProtectedLocalStore;
        }
        public async Task ClearToken()
        {
            await _protectedLocalStore.DeleteAsync(ApplicationConstants.AuthTokenName);
        }

        public async Task<string?> GetToken()
        {
            var result =
                await _protectedLocalStore.GetAsync<string>(ApplicationConstants.AuthTokenName);
            var token = result.Success ? result.Value : string.Empty;
            return token;
        }

        public async Task SetToken(string token)
        {
            await _protectedLocalStore.SetAsync(ApplicationConstants.AuthTokenName, token);
        }
    }
}
