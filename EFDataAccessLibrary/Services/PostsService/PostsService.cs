using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.PostsService;

public class PostsService : IPostsService
{
    private readonly DatabaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostsService(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        try
        {
            var posts = await _context.Posts
                .Include(c => c.comments)
                .Include(l => l.postLikes)
                .OrderByDescending(t => t.timestamp)
                .ToListAsync();
            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Post>> GetPostsFromOneUser(string username)
    {
        try
        {
            var posts = await _context.Posts
                .Where(u => u.Username.Equals(username))
                .Include(c => c.comments!)
                .Include(l => l.postLikes)
                .OrderByDescending(t => t.timestamp)
                .ToListAsync();
            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Post>> GetPostsFromOneHobby(string hobby)
    {
        try
        {
            var posts = await _context.Posts
                .Where(h => h.HobbyName.Equals(hobby))
                .Include(c => c.comments)
                .ThenInclude(u => u.user)
                .Include(l => l.postLikes)
                .OrderByDescending(t => t.timestamp)
                .ToListAsync();
            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    // ------------------------------------------------------------------------
    // Trying pagination
    public async Task<IEnumerable<Post>> GetPaginatedPosts(int page, int pageSize)
    {
        string LoggedInUsername = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName)!;
        try
        {
            var posts = await _context.Posts
                .Include(c => c.comments)
                .ThenInclude(u => u.user)
                .Include(l => l.postLikes)
                .OrderByDescending(t => t.timestamp)
                .Skip((page - 1) * pageSize) // Skip the previous pages
                .Take(pageSize) // Take the desired number of posts
                .ToListAsync();

            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Post>> GetPaginatedPostsFromOneHobby(int page, int pageSize, string hobby)
    {
        string LoggedInUsername = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName)!;
        try
        {
            var posts = await _context.Posts
                .Where(h => h.HobbyName.Equals(hobby))
                .Include(c => c.comments)
                .ThenInclude(u => u.user)
                .Include(l => l.postLikes)
                .OrderByDescending(t => t.timestamp)
                .Skip((page - 1) * pageSize) // Skip the previous pages
                .Take(pageSize) // Take the desired number of posts
                .ToListAsync();

            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }
    // ------------------------------------------------------------------------

    public async Task<IEnumerable<Post>> SearchPost(string searchTerm)
    {
        try
        {
            var posts = await _context.Posts
                .Where(b => b.body.Contains(searchTerm))
                .ToListAsync();
            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Post> GetOnePost(int id)
    {
        try
        {
            var post = await _context.Posts
                .Where(i => i.Id.Equals(id))
                .Include(c => c.comments)
                .Include(l => l.postLikes)
                .FirstOrDefaultAsync();
            return post;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Post> CreatePostByUser(PostCreateDTO postCreateDTO)
    {
        var user = await _context.Users //.FindAsync(post.Username);
            .Where(u => u.username.Equals(postCreateDTO.Username))
            .FirstOrDefaultAsync();
        var hobby = await _context.Hobbies //.FindAsync(post.HobbyName);
            .Where(h => h.hobbyName.Equals(postCreateDTO.HobbyName))
            .FirstOrDefaultAsync();

        // Checking to see if The logged in user is the same as the one who does the post creation
        if (_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName) 
            != postCreateDTO.Username)
        {
            return null;
        }

        if (user is null || hobby is null) 
        {
            return null;
        }
        else
        {
            try
            {
                Post post = new();
                post.Username = postCreateDTO.Username;
                post.HobbyName = postCreateDTO.HobbyName;
                post.body = postCreateDTO.body;
                post.timestamp = DateTime.Now;
                post.image = postCreateDTO.image;

                await _context.AddAsync(post);
                await _context.SaveChangesAsync();

                return post;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<string> EditPost(PostEditDTO postEditDTO)
    {
        var post = await _context.Posts.FindAsync(postEditDTO.Id);

        if (post is null)
        {
            return null;
        }
        else
        {
            // Checking to see if The logged in user is the same
            // as the one who did the original post creation
            if (_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName)
                != post.Username)
            {
                return null;
            }
            else
            {
                try
                {
                    await _context.Posts
                        .Where(i => i.Id.Equals(post.Id))
                        .ExecuteUpdateAsync(s => s
                            .SetProperty(b => b.body, b => postEditDTO.body)
                        );

                    return $"Post with Id: {postEditDTO.Id} has been edited successfully";
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                    return null;
                }
            }
        }
    }

    public async Task<string> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post is null)
        {
            return null;
        }
        else
        {
            try
            {
                await _context.Posts
                    .Where(i => i.Id.Equals(post.Id))
                    .ExecuteDeleteAsync();

                return $"Post with Id: {id} has been deleted successfully";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<Post> LikePost(LikePostDTO likePostDTO)
    {
        try
        {
            var post = await _context.Posts
            .Where(i => i.Id.Equals(likePostDTO.postId))
            .Include(pl => pl.postLikes)
            .FirstOrDefaultAsync();

            var user = await _context.Users
                .FindAsync(likePostDTO.userId);

            Like like = new();
            like.user =  user;

            await _context.AddAsync(like);

            post.postLikes.Add(like);
            //await _context.AddAsync(post.postLikes);

            await _context.SaveChangesAsync();

            return post;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<Post> CommentPost(CommentDTO commentDTO)
    {
        try
        {
            var post = await _context.Posts
            .Where(i => i.Id.Equals(commentDTO.postId))
            .Include(c => c.comments)
            .FirstOrDefaultAsync();

            var user = await _context.Users
                .Where(u => u.username.Equals(commentDTO.username))
                .FirstOrDefaultAsync();

            if(user is null || post is null)
            {
                return null!;
            }

            Comment comment = new();
            comment.user = user!;
            comment.body = commentDTO.body;

            await _context.AddAsync(comment);

            foreach (var item in post.comments)
            {
                await Console.Out.WriteLineAsync(item.body);
            }

            post.comments.Add(comment);

            await _context.SaveChangesAsync();

            return post;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
