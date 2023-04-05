using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HobbyNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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

        // POST api/Users/RegisterAdmin/5
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> CreateAdminUser(int id)
        {
            var response = await _usersService.CreateAdminUser(id);
            if(response == null) 
            {
                return NotFound($"The user with id: {id} was not found");
            }
            else
            {
                return Ok(response);
            }
        }

        // PUT api/Users
        [HttpPut]
        public async Task<ActionResult> EditUser(UserEditDTO userEditDTO)
        {
            var response = await _usersService.EditUser(userEditDTO);
            if (response == null)
            {
                return NotFound($"The user with id: {userEditDTO.Id} was not found");
            }
            else
            {
                return Ok(response);
            }
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteUser(int id)
        {
            return "value";
        }
    }
}
