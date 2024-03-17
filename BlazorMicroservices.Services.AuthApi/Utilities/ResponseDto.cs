namespace BlazorMicroservices.Services.AuthApi.Utilities
{
    public class ResponseDto
    {
        public bool IsSuccessful { get; set; } = true;
        public object? Result { get; set; }
        public string Message { get; set; } = string.Empty;
        //public IEnumerable<string> Errors { get; set; }
    }
}
