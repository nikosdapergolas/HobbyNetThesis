using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HobbyNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// This Function Uses the BCrypt library to hash the given password 
        /// and save the hashed password as well as the given username, email, firstname, lastname
        /// </summary>
        /// <param name="request">This is the body in the post method that contains 
        /// a username, a password, an email, a firstname and a lastname</param>
        /// <returns>Returns the User object that is crated and saved.</returns>
        [HttpPost("register")]
        public ActionResult<User> Register(UserSignUpDTO request)
        {
            var registeredUser = _authService.Register(request);
            return Ok(registeredUser);
        }

        /// <summary>
        /// This Function Uses the BCrypt library to hash the given password and check to see
        /// if the given username and password exists.
        /// </summary>
        /// <param name="request">Takes the username and password given</param>
        /// <returns>If the user exists, it returns a Jason Web Token for this user</returns>
        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDTO request)
        {
            var token = _authService.Login(request);

            if(token.Equals("Not found"))
            {
                return BadRequest("Username or Password is incorrect!!");
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
