using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.PostsService;

public class PostsService : IPostsService
{
    private readonly DatabaseContext _context;

    public PostsService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var posts = await _context.Posts
            .Include(c => c.comments)
            .Include(l => l.postLikes)
            .ToListAsync();
        return posts;

    }

    public async Task<IEnumerable<Post>> SearchPost(string searchTerm)
    {
        var posts = await _context.Posts
            .Where(b => b.body.Contains(searchTerm))
            .ToListAsync();
        return posts;
    }

    public async Task<Post> GetOnePost(int id)
    {
        var post = await _context.Posts
            .Where(i => i.Id.Equals(id))
            .Include(c => c.comments)
            .Include (l => l.postLikes)
            .FirstOrDefaultAsync();
        return post;
    }

    public async Task<Post> CreatePostByUser(Post post)
    {
        var user = await _context.Users.FindAsync(post.user);
        var hobby = await _context.Hobbies.FindAsync(post.hobby);

        if (user is null || hobby is null) 
        {
            return null;
        }
        else
        {
            return post;
        }
    }

    public async Task<Post> CreatePostByAdmin(Post post)
    {
        var user = await _context.Users.FindAsync(post.user);
        var hobby = await _context.Hobbies.FindAsync(post.hobby);

        if (user is null || hobby is null)
        {
            return null;
        }
        else
        {
            return post;
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
            await _context.Posts
                .Where(i => i.Id.Equals(post.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.body, b => postEditDTO.body)
                );

            return $"Post with Id: {postEditDTO.Id} has been edited successfully";
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
            await _context.Posts
                .Where(i => i.Id.Equals(post.Id))
                .ExecuteDeleteAsync();

            return $"Post with Id: {id} has been deleted successfully";
        }
    }
}
