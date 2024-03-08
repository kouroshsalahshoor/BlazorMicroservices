using Microsoft.AspNetCore.Identity;

namespace BlazorMicroservices.Services.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
