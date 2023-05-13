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
        return Ok(hobbies);
    }

    // GET: api/Hobbies/Search/?searchTerm=abc
    [HttpGet("Search")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Hobby>>> SearchHobby(string searchTerm)
    {
        var hobbies = await _service.SearchHobby(searchTerm);

        if (hobbies.Count().Equals(0))
        {
            return NotFound($"There are no hobbies that match the search term: {searchTerm}");
        }
        else
        {
            return Ok(hobbies);
        }
    }

    // GET api/Hobbies/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Hobby>> GetOneHobby(int id)
    {
        var hobby = await _service.GetOneHobby(id);
        if (hobby == null)
        {
            return NotFound($"The hobby with id: {id} was not found");
        }
        else
        {
            return Ok(hobby);
        }
    }

    // POST api/Hobbies
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateHobby(Hobby hobby)
    {
        var response = await _service.CreateHobby(hobby);

        if (response == null)
        {
            // That means that this hobby already exists
            return BadRequest();
        }
        else
        {
            return Ok(response);
        }
    }

    // PUT api/Hobbies/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> EditHobby(Hobby hobby)
    {
        var response = await _service.EditHobby(hobby);
        if (response == null)
        {
            return NotFound($"The hobby with id: {hobby.Id} was not found");
        }
        else
        {
            return Ok(response);
        }
    }

    // DELETE api/Hobbies/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteHobby(int id)
    {
        var response = await _service.DeleteHobby(id);
        if (response == null)
        {
            return NotFound($"The hobby with id: {id} was not found");
        }
        else
        {
            return Ok(response);
        }
    }
}
