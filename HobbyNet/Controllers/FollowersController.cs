using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.Followers2Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class FollowersController : ControllerBase
{
    private readonly IFollowersService _followersService;

    public FollowersController(IFollowersService followersService)
    {
        _followersService = followersService;
    }

    // GET: api/Followers
    [HttpGet]
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

    // GET: api/Followers/User/
    [HttpGet("User")]
    public async Task<ActionResult<IEnumerable<string>>> GetFollowersOfOnePerson(string username)
    {
        var followers = await _followersService.GetFollowersOfOnePerson(username);

        if (followers is not null)
        {
            return Ok(followers);
        }
        else
        {
            return NotFound();
        }
    }

    // POST: api/Followers/follow
    [HttpPost("follow")]
    public async Task<ActionResult<IEnumerable<Followers>>> FollowAPerson(FollowersDTO followersDTO)
    {
        var followRequest = await _followersService.FollowAPerson(followersDTO);

        if (followRequest is not null)
        {
            return Ok(followRequest);
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE: api/Followers/unfollow
    [HttpDelete("unfollow")]
    public async Task<ActionResult<IEnumerable<Followers>>> UnfollowAPerson(FollowersDTO followersDTO)
    {
        var followRequest = await _followersService.UnfollowAPerson(followersDTO);

        if (followRequest is not null)
        {
            return Ok(followRequest);
        }
        else
        {
            return BadRequest();
        }
    }
}
