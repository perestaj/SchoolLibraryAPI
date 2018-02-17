using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL.Entities
{
    public partial class Author
    {
        public Author()
        {
            BookAuthor = new HashSet<BookAuthor>();
        }

        public int AuthorId { get; set; }
        public string AdditionalInformation { get; set; }
        public string FirstName { get; set; }
        public bool? IsDeleted { get; set; }
        public string LastName { get; set; }

        public ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
