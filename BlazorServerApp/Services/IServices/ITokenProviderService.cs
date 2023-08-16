namespace BlazorServerApp.Services
{
    public interface ITokenProviderService
    {
        Task SetToken(string token);
        Task<string?> GetToken();
        Task ClearToken();
    }
}
