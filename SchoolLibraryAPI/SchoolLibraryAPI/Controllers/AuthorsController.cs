using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SchoolLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authors = _authorService.Get();
            return Ok(authors);
        }

        [HttpGet("{id}")]        
        public IActionResult Get(int id)
        {
            var author = _authorService.GetById(id);

            return author != null ? (IActionResult)Ok(author) : BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Post([FromBody] AuthorModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _authorService.Update(model);
                return Ok(model);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _authorService.Delete(id);
                return Ok(id);
            }

            return BadRequest();
        }
    }
}