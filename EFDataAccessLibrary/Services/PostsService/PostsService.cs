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

    public async Task<string> CreatePostByAdmin()
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreatePostByUser()
    {
        throw new NotImplementedException();
    }

    public async Task<string> EditPost(PostEditDTO postEditDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<string> DeletePost(int id)
    {
        throw new NotImplementedException();
    }
}
