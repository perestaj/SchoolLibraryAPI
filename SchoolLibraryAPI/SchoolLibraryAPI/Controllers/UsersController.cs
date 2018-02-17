using System;
using System.Collections.Generic;
using SchoolLibraryAPI.Common;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SchoolLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Get()
        {
            var users = _userService.Get();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetById(id);
            return user != null ? (IActionResult)Ok(user) : BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Post([FromBody] UserModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var result = _userService.Update(model);
                return Ok(result);                                
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = " Administrator")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _userService.Delete(id);
                return Ok(id);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("/api/users/roles")]
        public IActionResult GetRoles()
        {
            var roles = new List<RoleModel>();
            var roleValues = Enum.GetValues(typeof(Role));
            foreach (int role in roleValues)
            {
                roles.Add(new RoleModel
                {
                    Id = role,
                    Name = ((Role)role).ToString()
                });
            }

            return Ok(roles);
        }

        [HttpPost("token")]
        public IActionResult CreateToken(string userName, string password)
        {
            var token = _userService.GetToken(userName, password);
            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }
    }
}