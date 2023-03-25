using EFDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestUsersController : ControllerBase
{

    // GET: api/TestUsers
    [HttpGet]
    [Authorize]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/TestUsers/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"value {id}";
    }

    // POST api/TestUsers
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/TestUsers/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/TestUsers/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
