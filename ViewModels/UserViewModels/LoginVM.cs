using System.ComponentModel.DataAnnotations;

namespace SafeCam.ViewModels.UserViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(512), MinLength(3), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(512), MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
