using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test.DAL.Repositories.UserRepo;
using Test.DOM.Models;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(
            IUserRepo userRepo, 
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _userRepo = userRepo;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userDto.EmployeeNo);
                //var user = _userRepo.Authenticate(userDto.EmployeeNo, userDto.Password);
                if (user is null) return BadRequest();

                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, user.EmployeeNo),
                //    new Claim(ClaimTypes.Email, user.Email)
                //};

                //var claimsIdentity = new
                //    ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var authProperties = new AuthenticationProperties
                //{
                //    // Options
                //};

                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.EmployeeNo));

                await _signInManager.SignInAsync(user, isPersistent: false);

                //Response.Cookies.Append("username", user.EmployeeNo);

                // Map to Dto
                var userReturn = new UserDto
                {
                    EmployeeNo = user.EmployeeNo,
                    FirstName = user.FirstName,
                    MiddleInitial = user.MiddleInitial,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserId = user.Id
                };

                return Ok(userReturn);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                // Map UserDto to User
                var user = new User
                {
                    UserName = userDto.EmployeeNo,
                    EmployeeNo = userDto.EmployeeNo,
                    FirstName = userDto.FirstName,
                    MiddleInitial = userDto.MiddleInitial,
                    LastName = userDto.LastName,
                    Email = userDto.Email
                };

                var registerResult = await _userManager.CreateAsync(user, userDto.Password);

                if (!registerResult.Succeeded)
                {
                    throw new Exception($"The user couldn't be created - {registerResult}");
                }

                //_userRepo.Create(user, userDto.Password);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            var test = _signInManager.IsSignedIn(this.User);
            return Ok(new { data = "successful" });
        }
    }
}