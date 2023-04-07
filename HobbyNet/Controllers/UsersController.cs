using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/Users/Search/?searchTerm=abc
        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string searchTerm)
        {
            var users = await _usersService.SearchUsers(searchTerm);

            if (users.Count().Equals(0))
            {
                return NotFound($"There are no users that match the search term: {searchTerm}");
            }
            else
            {
                return Ok(users);
            }            
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _usersService.DeleteUser(id);
            if (response == null)
            {
                return NotFound($"The user with id: {id} was not found");
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
