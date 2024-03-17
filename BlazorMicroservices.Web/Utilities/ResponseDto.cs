namespace BlazorMicroservices.Web.Utilities
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; }
    }
}
