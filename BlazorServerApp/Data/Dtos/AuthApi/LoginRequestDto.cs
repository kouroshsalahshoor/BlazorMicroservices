using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Data
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage ="{0} is required")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }
}
