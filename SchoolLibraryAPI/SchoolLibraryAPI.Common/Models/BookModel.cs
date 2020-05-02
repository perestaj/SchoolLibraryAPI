using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolLibraryAPI.Common.Models
{
    public class BookModel : IValidatableObject
    {
        public int BookID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Field cannot contain more than 100 characters")]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000, ErrorMessage = "Field cannot contain more than 1000 characters")]
        public string AdditionalInformation { get; set; }

        [Required]
        public int? PublisherID { get; set; }
        public PublisherModel Publisher { get; set; }

        public BookStatus Status { get; set; }

        public IList<AuthorModel> Authors { get; set; }

        [Required]
        public IList<int> AuthorIds { get; set; }

        public bool IsDeleted { get; set; }

        public string AuthorsList => Authors != null ? string.Join(", ", Authors.Select(x => x.FullName)) : string.Empty;
        public string PublisherName => Publisher != null ? Publisher.Name : string.Empty;
        public string StatusName => Status.ToString();

        public BookModel()
        {
            Authors = new List<AuthorModel>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AuthorIds == null || !AuthorIds.Any())
            {
                yield return new ValidationResult("No authors defined", new[] { nameof(AuthorIds) });
            }
        }
    }
}
