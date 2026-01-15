using Microsoft.AspNetCore.Identity;

namespace SafeCam.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
