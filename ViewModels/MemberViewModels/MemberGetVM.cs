namespace SafeCam.ViewModels.MemberViewModels
{
    public class MemberGetVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
