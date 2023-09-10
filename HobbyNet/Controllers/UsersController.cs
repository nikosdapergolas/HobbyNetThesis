using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersController(
        IUsersService usersService,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration config, 
        IHttpContextAccessor httpContextAccessor)
    {
        _usersService = usersService;
        _webHostEnvironment = webHostEnvironment;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET: api/Users
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _usersService.GetAllUsers();

        if (users is not null)
        {
            return Ok(users);
        }
        else
        {
            return BadRequest();
        }
    }

    // GET: api/Users/Search/?searchTerm=abc
    [HttpGet("Search")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string searchTerm)
    {
        var users = await _usersService.SearchUsers(searchTerm);

        if (users.Count().Equals(0))
        {
            return NotFound();
        }
        else
        {
            return Ok(users);
        }            
    }

    // GET api/Users/Username
    [HttpGet("Username/{username}")]
    [Authorize]
    public async Task<ActionResult<User>> GetOneUserByUsername(string username)
    {
        var user = await _usersService.GetOneUserByUsername(username);

        if (user is not null)
        {
            return Ok(user);
        }
        else
        {
            return NotFound();
        }
    }

    // GET api/Users/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<User>> GetOneUser(int id)
    {
        var user = await _usersService.GetOneUser(id);

        if (user is not null)
        {
            return Ok(user);
        }
        else
        {
            return NotFound();
        }
    }

    // POST api/Users/RegisterAdmin/5
    [HttpPost("RegisterAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateAdminUser(int id)
    {
        var response = await _usersService.CreateAdminUser(id);

        if(response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // PUT api/Users
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> EditUser(UserEditDTO userEditDTO)
    {
        var response = await _usersService.EditUser(userEditDTO);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/Users/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var response = await _usersService.DeleteUser(id);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("TestUploadImageFile")]
    [Authorize]
    public async Task<ActionResult<string>> TestUploadFile(IFormFile file)
    {
        var path = Path.Combine(
            //_config.GetValue<string>("FileStorage"),
            "S:\\visual_studio_projects\\web_projects\\Thesis\\HobbyNet\\wwwroot\\images\\profile_pictures",
            _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.GivenName) + // This is the logged in user's username
            "_" +
            file.Name);

        await using FileStream fs = new(path, FileMode.Create);
        await file.CopyToAsync(fs);

        return Ok(path);
    }

        [HttpPost("uploadImageFile")]
    [Authorize]
    public async Task<ActionResult<UploadResult>> UploadFile(IFormFile file)
    {
        UploadResult uploadResult = new();
        string trustedFileNameForFileStorage = string.Empty;
        string untrustedFileName = file.Name;
        uploadResult.FileName = untrustedFileName;
        var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

        //trustedFileNameForFileStorage = Path.GetRandomFileName();
        //----------------------------------------------------------
        //trustedFileNameForFileStorage = Path.ChangeExtension(
        //        Path.GetRandomFileName(),
        //        Path.GetExtension(file.Name));
        //----------------------------------------------------------
        var path = Path.Combine(
            _config.GetValue<string>("FileStorage"),
            _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.GivenName) + // This is the logged in user's username
            "_" +
            //trustedFileNameForFileStorage);
            file.FileName);

        await using FileStream fs = new(path, FileMode.Create);
        await file.CopyToAsync(fs);

        uploadResult.StoredFileName = file.FileName;

        return Ok(uploadResult);
    }
}
