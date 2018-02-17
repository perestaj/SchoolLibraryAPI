using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL.Entities
{
    public partial class Book
    {
        public Book()
        {
            BookAuthor = new HashSet<BookAuthor>();
            Loans = new HashSet<Loan>();
        }

        public int BookId { get; set; }
        public string AdditionalInformation { get; set; }
        public int BookStatus { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LoanId { get; set; }
        public int PublisherId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }

        public Loan Loan { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
