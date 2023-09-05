using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services.Followers2Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowersController : ControllerBase
{
    private readonly IFollowersService _followersService;

    public FollowersController(IFollowersService followersService)
    {
        _followersService = followersService;
    }

    // GET: api/Followers
    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<IEnumerable<Followers>>> GetAllFollowers()
    {
        var followers = await _followersService.GetAllFollowers();

        if (followers is not null)
        {
            return Ok(followers);
        }
        else
        {
            return BadRequest();
        }
    }
}
