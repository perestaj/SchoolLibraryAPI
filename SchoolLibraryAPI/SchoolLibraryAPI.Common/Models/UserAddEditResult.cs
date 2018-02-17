namespace SchoolLibraryAPI.Common.Models
{
    public class UserAddEditResult
    {
        public bool Success { get; set; }
        public bool UserNameTaken { get; set; }
        public bool EmailTaken { get; set; }
        public int UserID { get; set; }
    }
}
