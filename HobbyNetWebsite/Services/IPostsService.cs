using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();

    public Task<List<Post>> GetPostsFromOneUser(string username);

    public Task<List<Post>> GetSomePosts(int currentPage);

    Task<string> CreatePost(PostCreateDTO postCreateDTO);

    Task<string> DeletePost(int postId);
}
