using HobbyNetWebApp.Models.Resused;

namespace HobbyNetWebApp.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();
}
