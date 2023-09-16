using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.HobbiesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HobbiesController : ControllerBase
{
    private readonly IHobbiesService _service;

    public HobbiesController(IHobbiesService service)
    {
        _service = service;
    }

    // GET: api/Hobbies
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Hobby>>> GetAllHobbies()
    {
        var hobbies = await _service.GetAllHobbies();

        if (hobbies is not null)
        {
            return Ok(hobbies);
        }
        else
        {
            return BadRequest();
        }
    }

    // GET: api/Hobbies/Search/?searchTerm=abc
    [HttpGet("Search")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Hobby>>> SearchHobby(string searchTerm)
    {
        var hobbies = await _service.SearchHobby(searchTerm);

        if (hobbies is not null)
        {
            if (hobbies.Count().Equals(0))
            {
                return NotFound();
            }
            else
            {

                return Ok(hobbies);
            }
        }
        else
        {
            return BadRequest();
        }
    }

    // GET api/Hobbies/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Hobby>> GetOneHobby(int id)
    {
        var hobby = await _service.GetOneHobby(id);

        if (hobby is not null)
        {
            return Ok(hobby);
        }
        else
        {
            return NotFound();
        }
    }

    // POST api/Hobbies
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateHobby(Hobby hobby)
    {
        var response = await _service.CreateHobby(hobby);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // PUT api/Hobbies/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> EditHobby(Hobby hobby)
    {
        var response = await _service.EditHobby(hobby);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/Hobbies/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteHobby(int id)
    {
        var response = await _service.DeleteHobby(id);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("followHobby")]
    [Authorize]
    public async Task<ActionResult> FollowAHobby(FollowHobbyDTO followHobbyDTO)
    {
        var response = await _service.FollowAHobby(followHobbyDTO);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost("unfollowHobby")]
    [Authorize]
    public async Task<ActionResult> UnfollowAHobby(FollowHobbyDTO followHobbyDTO)
    {
        var response = await _service.UnfollowAHobby(followHobbyDTO);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
}
