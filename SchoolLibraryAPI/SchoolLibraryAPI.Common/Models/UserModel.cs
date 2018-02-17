using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibraryAPI.Common.Models
{
    public class UserModel : IValidatableObject
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string UserName { get; set; }

        [StringLength(128, ErrorMessage = "Field cannot contain more than 128 characters")]
        public string Password { get; set; }

        [StringLength(128, ErrorMessage = "Field cannot contain more than 128 characters")]        
        public string PasswordConfirm { get; set; }

        [Required]
        public string Role { get; set; }

        public string RoleName
        {
            get
            {
                SchoolLibraryAPI.Common.Role role;
                if (!string.IsNullOrEmpty(Role) && Enum.TryParse<SchoolLibraryAPI.Common.Role>(Role, out role))
                {
                    return role.ToString();
                }

                return string.Empty;
            }
        }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Field cannot contain more than 50 characters")]
        public string LastName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [EmailAddress]
        [StringLength(100, ErrorMessage = "Field cannot contain more than 100 characters")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Field cannot contain more than 200 characters")]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(PasswordConfirm))
            {
                if (!string.Equals(Password ?? string.Empty, PasswordConfirm ?? string.Empty))
                {
                    yield return new ValidationResult("Password and Password Confirm must match", new[] { nameof(PasswordConfirm) });
                }
            }

            if (UserID == 0 && string.IsNullOrEmpty(Password))
            {
                yield return new ValidationResult("Password is required", new[] { nameof(Password) });
            }

            SchoolLibraryAPI.Common.Role role;
            if (!string.IsNullOrEmpty(Role) && !Enum.TryParse<SchoolLibraryAPI.Common.Role>(Role, out role))
            {
                yield return new ValidationResult("Invalid role", new[] { nameof(Role) });
            }
        }
    }
}
