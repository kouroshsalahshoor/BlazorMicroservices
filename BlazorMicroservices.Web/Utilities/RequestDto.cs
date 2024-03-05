using static BlazorMicroservices.Web.Utilities.SD;

namespace BlazorMicroservices.Web.Utilities
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
