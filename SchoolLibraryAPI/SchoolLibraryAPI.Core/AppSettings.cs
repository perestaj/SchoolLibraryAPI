using System;

namespace SchoolLibraryAPI.Core
{
    public class AppSettings
    {
        public string SymmetricSecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpirationInMinutes { get; set; }
        public string ClaimTypeUserName { get; set; }
    }
}
