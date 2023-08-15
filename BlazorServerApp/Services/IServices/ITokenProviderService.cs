namespace BlazorServerApp.Services
{
    public interface ITokenProviderService
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
