using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.PostsService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _postsService.GetAllPosts();
        return Ok(posts);
    }

    // GET api/Posts/5
    [HttpGet("{id}")]
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

    // POST api/Posts/AdminPost
    [HttpPost("AdminPost")]
    public async Task<ActionResult> CreatePostByAdmin()
    {
        var response = await _postsService.CreatePostByAdmin();
        return Ok(response);
    }

    // POST api/Posts/UserPost
    [HttpPost("UserPost")]
    public async Task<ActionResult> CreatePostByUser()
    {
        var response = await _postsService.CreatePostByUser();
        return Ok(response);
    }

    // PUT api/Posts/5
    [HttpPut("{id}")]
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
