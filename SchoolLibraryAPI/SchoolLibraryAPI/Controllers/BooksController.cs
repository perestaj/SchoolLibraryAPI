using SchoolLibraryAPI.Common;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SchoolLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookService.Get();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetById(id);
            return book != null ? (IActionResult)Ok(book) : BadRequest();
        }

        [HttpGet]
        [Route("/api/books/statuses")]
        public IActionResult GetBookStatuses()
        {
            var statuses = new List<BookStatusModel>();
            var statusValues = Enum.GetValues(typeof(BookStatus));
            foreach (int status in statusValues)
            {
                statuses.Add(new BookStatusModel
                {
                    Id = status,
                    Name = ((BookStatus)status).ToString()
                });
            }

            return Ok(statuses);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Post([FromBody] BookModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _bookService.Update(model);
                return Ok(model);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Delete(int id)
        {            
            _bookService.Delete(id);

            return Ok(id);
        }
    }
}