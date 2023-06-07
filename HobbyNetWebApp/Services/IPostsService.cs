using HobbyNetWebApp.Models;
using HobbyNetWebApp.Models.Resused;

namespace HobbyNetWebApp.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();

    Task<string> CreatePost(PostCreateDTO postCreateDTO);
}
