using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HobbyNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();
            return Ok(users);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetOneUser(int id)
        {
            var user = await _usersService.GetOneUser(id);
            if (user == null)
            {
                return NotFound($"The user with id: {id} was not found");
            }
            else
            {
                return Ok(user);
            }
        }

        // POST api/Users
        [HttpPost]
        public async Task<string> CreateAdminUser([FromBody] string value)
        {
            return "value";
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<string> EditUser(int id, [FromBody] string value)
        {
            return "value";
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteUser(int id)
        {
            return "value";
        }
    }
}
