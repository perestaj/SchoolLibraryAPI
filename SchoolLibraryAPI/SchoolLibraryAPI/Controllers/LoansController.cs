using SchoolLibraryAPI.Common;
using SchoolLibraryAPI.Core;
using SchoolLibraryAPI.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SchoolLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]    
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IOptions<AppSettings> _options;

        public LoansController(ILoanService loanService, IOptions<AppSettings> options)
        {
            _loanService = loanService;
            _options = options;
        }

        [HttpGet]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Get()
        {
            var loans = _loanService.Get();
            return Ok(loans);
        }

        [HttpPost("/api/loans/update/{userID}/{bookID}/{bookStatus}")]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Post(int userID, int bookID, BookStatus bookStatus)
        {
            var username = User.FindFirst(_options.Value.ClaimTypeUserName).Value;

            var result = _loanService.UpdateBookStatus(userID, bookID, bookStatus);

            return result ? (IActionResult)Ok(bookID) : BadRequest();
        }

        [HttpPost("/api/loans/request")]
        [Authorize]
        public IActionResult RequestBook(int bookID)
        {
            var username = User.FindFirst(_options.Value.ClaimTypeUserName).Value;

            var result = _loanService.RequestBook(username, bookID);

            return result ? (IActionResult)Ok(bookID) : BadRequest();
        }
    }
}
