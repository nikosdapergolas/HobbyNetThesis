using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.PostsService;

public class PostsService : IPostsService
{
    public async Task<string> CreatePostByAdmin()
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreatePostByUser()
    {
        throw new NotImplementedException();
    }

    public async Task<string> DeletePost(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> EditPost(PostEditDTO postEditDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllPosts()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetOnePost(int id)
    {
        throw new NotImplementedException();
    }
}
