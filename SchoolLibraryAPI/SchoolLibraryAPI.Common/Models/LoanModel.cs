using System;

namespace SchoolLibraryAPI.Common.Models
{
    public class LoanModel
    {
        public int LoanID { get; set; }

        public int BookID { get; set; }
        public BookModel Book { get; set; }

        public int UserID { get; set; }
        public UserModel User { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
