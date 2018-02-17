using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SchoolLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var publishers = _publisherService.Get();
            return Ok(publishers);
        }

        [HttpGet("{id}")]        
        public IActionResult Get(int id)
        {
            var publisher = _publisherService.GetById(id);

            return publisher != null ? (IActionResult)Ok(publisher) : BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Librarian, Administrator")]
        public IActionResult Post([FromBody] PublisherModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _publisherService.Update(model);
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
                _publisherService.Delete(id);
                return Ok(id);
            }

            return BadRequest();
        }
    }
}