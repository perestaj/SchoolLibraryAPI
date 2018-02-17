using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL.Entities
{
    public partial class Loan
    {
        public Loan()
        {
            Books = new HashSet<Book>();
        }

        public int LoanId { get; set; }
        public int BookId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
