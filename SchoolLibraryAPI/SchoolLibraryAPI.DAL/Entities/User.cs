using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Loans = new HashSet<Loan>();
        }

        public int UserId { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public bool? IsDeleted { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int Role { get; set; }
        public string UserName { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
