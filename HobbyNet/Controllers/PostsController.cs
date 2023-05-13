using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.PostsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;

    public PostsController(IPostsService service)
    {
        _postsService = service;
    }

    // GET: api/Posts
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _postsService.GetAllPosts();
        return Ok(posts);
    }

    // GET: api/Posts/Search/?searchTerm=abc
    [HttpGet("Search")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> SearchPost(string searchTerm)
    {
        var posts = await _postsService.SearchPost(searchTerm);
        if (posts.Count().Equals(0))
        {
            return NotFound($"There are no posts that match the search term: {searchTerm}");
        }
        else
        {
            return Ok(posts);
        }
    }

    // GET api/Posts/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Post>> GetOnePost(int id)
    {
        var post = await _postsService.GetOnePost(id);
        if (post == null)
        {
            return NotFound($"The post with id: {id} was not found");
        }
        else
        {
            return Ok(post);
        }
    }

    // POST api/Posts/UserPost
    [HttpPost("UserPost")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> CreatePostByUser(Post post)
    {
        var response = await _postsService.CreatePostByUser(post);

        if (response == null)
        {
            // User or Hobby does not exist
            return BadRequest();
        }
        else
        {
            return Ok(response);
        }
    }

    // POST api/Posts/AdminPost
    [HttpPost("AdminPost")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreatePostByAdmin(Post post)
    {
        var response = await _postsService.CreatePostByAdmin(post);

        if(response == null)
        {
            // User or Hobby does not exist
            return BadRequest();
        }
        else
        {
            return Ok(response);
        }
    }

    // PUT api/Posts/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> EditPost(PostEditDTO postEditDTO)
    {
        var response = await _postsService.EditPost(postEditDTO);
        if (response == null)
        {
            return NotFound($"The post with id: {postEditDTO.Id} was not found");
        }
        else
        {
            return Ok(response);
        }
    }

    // DELETE api/Posts/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var response = await _postsService.DeletePost(id);
        if(response == null)
        {
            return NotFound($"The post with id: {id} was not found");
        }
        else 
        {
            return Ok(response);
        }
    }
}
