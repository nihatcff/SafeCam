using SafeCam.Models.Common;

namespace SafeCam.Models
{
    public class Member : BaseEntity
    {
        public string Fullname { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
