namespace BlazorMicroservices.Web.Utilities.Auth
{
    public class RegisterResponseDto
    {
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
