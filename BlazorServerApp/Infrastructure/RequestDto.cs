using static BlazorServerApp.Infrastructure.SystemConstants;

namespace BlazorServerApp.Infrastructure
{
    public class RequestDto
    {
        public ApiTypes ApiType { get; set; } = ApiTypes.GET;
        public string Url { get; set; }
        public object? Data { get; set; }
        public string Token { get; set; }
    }
}
