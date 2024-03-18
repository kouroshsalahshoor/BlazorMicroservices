using BlazorMicroservices.Web.Utilities;
using BlazorMicroservices.Web.Utilities.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlazorMicroservices.Web.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public AuthStateProvider(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage)
        {
            _httpClient = httpClient;
            _protectedLocalStorage = protectedLocalStorage;
        }

        //https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-8.0&pivots=server
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var result = await _protectedLocalStorage.GetAsync<string>(SD.JwtToken);
                var token = result.Success ? result.Value : null;
                if (token is null)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
            }
            catch (Exception)
            {
                //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[]
                //{
                //    new Claim(ClaimTypes.Name, "abc@x.x")
                //}, "jwtAuthType")));
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void NotifyUserLoggedIn(string token)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}