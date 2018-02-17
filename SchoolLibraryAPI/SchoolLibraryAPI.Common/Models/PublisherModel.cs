using System.ComponentModel.DataAnnotations;

namespace SchoolLibraryAPI.Common.Models
{
    public class PublisherModel
    {
        public int PublisherID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Field cannot contain more than 200 characters")]
        public string Address { get; set; }

        [StringLength(1000, ErrorMessage = "Field cannot contain more than 1000 characters")]
        public string AdditionalInformation { get; set; }

        public bool IsDeleted { get; set; }
    }
}
