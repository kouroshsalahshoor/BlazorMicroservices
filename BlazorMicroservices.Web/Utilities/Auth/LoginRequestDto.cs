using System.ComponentModel.DataAnnotations;

namespace BlazorMicroservices.Web.Utilities.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
