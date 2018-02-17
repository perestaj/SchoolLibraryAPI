using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL.Entities
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string AdditionalInformation { get; set; }
        public string Address { get; set; }
        public bool? IsDeleted { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
