using System.ComponentModel.DataAnnotations;

namespace SafeCam.ViewModels.MemberViewModels
{
    public class MemberUpdateVM
    {
        public int Id { get; set; }
        [Required,MaxLength(512),MinLength(5)]
        public string Fullname { get; set; } = string.Empty;
        [Required,MaxLength(1024),MinLength(5)]
        public string Designation { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
