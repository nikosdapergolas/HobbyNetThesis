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

        if (posts is not null) 
        {
            return Ok(posts);
        }
        else
        {
            return BadRequest();
        }
    }

    // GET: api/Posts/User
    [HttpGet("User/{username}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostsFromOneUser(string username)
    {
        var posts = await _postsService.GetPostsFromOneUser(username);

        if (posts is not null)
        {
            return Ok(posts);
        }
        else
        {
            return BadRequest();
        }
    }

    // ------------------------------------------------------------------------
    // Trying pagination and also also only posts from users,
    // that the logged in user follows

    // GET: api/Posts/Some
    [HttpGet("Some/{page}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> GetSomePosts(int page = 1)
    {
        const int pageSize = 10; // Number of posts per page

        var posts = await _postsService.GetPaginatedPosts(page, pageSize);

        if (posts is not null)
        {
            return Ok(posts);
        }
        else
        {
            return BadRequest();
        }
    }
    // ------------------------------------------------------------------------

    // GET: api/Posts/Search/?searchTerm=abc
    [HttpGet("Search")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Post>>> SearchPost(string searchTerm)
    {
        var posts = await _postsService.SearchPost(searchTerm);
        if (posts.Count().Equals(0))
        {
            return NotFound();
        }
        else if(posts is null)
        {
            return BadRequest();
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
        if (post is not null)
        {
            return Ok(post);
        }
        else
        {
            return NotFound();
        }
    }

    // POST api/Posts/UserPost
    [HttpPost("UserPost")]
    [Authorize]
    public async Task<ActionResult> CreatePostByUser(PostCreateDTO post)
    {
        var response = await _postsService.CreatePostByUser(post);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // PUT api/Posts/5
    [HttpPut()]
    [Authorize]
    public async Task<ActionResult> EditPost(PostEditDTO postEditDTO)
    {
        var response = await _postsService.EditPost(postEditDTO);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/Posts/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeletePost(int id)
    {
        var response = await _postsService.DeletePost(id);

        if(response is not null)
        {
            return Ok(response);
        }
        else
        {
            return NotFound($"The post with id: {id} was not found");
        }
    }

    // POST api/Posts/like
    [HttpPost("like")]
    [Authorize]
    public async Task<ActionResult<Post>> LikePost(LikePostDTO likePostDTO)
    {
        var response = await _postsService.LikePost(likePostDTO);

        if(response is not null)
        {
            return Ok(response);
        }
        else 
        { 
            return BadRequest(); 
        }
    }

    // POST api/Posts/comment
    [HttpPost("comment")]
    [Authorize]
    public async Task<ActionResult<Post>> CommentPost(CommentDTO commentDTO)
    {
        var response = await _postsService.CommentPost(commentDTO);

        if (response is not null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }
}
