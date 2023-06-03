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
        try
        {
            var posts = await _context.Posts
                .Include(c => c.comments)
                .Include(l => l.postLikes)
                .ToListAsync();
            return posts;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

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

        // Prepei na prosthesw allo ena check, oti o xrhsthw pou exei kanei log in 
        // me ton xrhsth pou leei to object oti exei kanei to post, einai o idios xrisths
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

    //public async Task<Post> CreatePostByAdmin(PostCreateDTO postCreateDTO)
    //{
    //    var user = await _context.Users.FindAsync(postCreateDTO.Username);
    //    var hobby = await _context.Hobbies.FindAsync(postCreateDTO.HobbyName);

    //    if (user is null || hobby is null)
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        try
    //        {
    //            Post post = new();
    //            post.Username = postCreateDTO.Username;
    //            post.HobbyName = postCreateDTO.HobbyName;
    //            post.body = postCreateDTO.body;

    //            await _context.AddAsync(post);
    //            await _context.SaveChangesAsync();

    //            return post;
    //        }
    //        catch (Exception ex)
    //        {
    //            await Console.Out.WriteLineAsync(ex.Message);
    //            return null;
    //        }
    //    } 
    //}

    public async Task<string> EditPost(PostEditDTO postEditDTO)
    {
        var post = await _context.Posts.FindAsync(postEditDTO.Id);

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
}
