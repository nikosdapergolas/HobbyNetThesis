using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();

    Task<string> CreatePost(PostCreateDTO postCreateDTO);
}
