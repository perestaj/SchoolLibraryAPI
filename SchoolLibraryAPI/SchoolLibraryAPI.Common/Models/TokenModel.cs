using System;

namespace SchoolLibraryAPI.Common.Models
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string UserName { get; set; }

        public int Role { get; set; }
    }
}
