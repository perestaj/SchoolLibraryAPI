using System.ComponentModel.DataAnnotations;

namespace SchoolLibraryAPI.Common.Models
{
    public class AuthorModel
    {
        public int AuthorID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string LastName { get; set; }

        [StringLength(1000, ErrorMessage = "Field cannot contain more than 1000 characters")]
        public string AdditionalInformation { get; set; }

        public bool IsDeleted { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
