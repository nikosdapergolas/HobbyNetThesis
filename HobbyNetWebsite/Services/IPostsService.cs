using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();

    Task<Post> GetOnePost(int postId);

    public Task<List<Post>> GetPostsFromOneUser(string username);

    Task<List<Post>> GetPostsFromOneHobby(string hobby);

    public Task<List<Post>> GetSomePosts(int currentPage);

    Task<List<Post>> GetSomePostsFromOneHobby(int currentPage, string hobby);

    Task<string> CreatePost(PostCreateDTO postCreateDTO);

    Task<string> DeletePost(int postId);

    Task<string> EditPost(PostEditDTO postEditDTO);

    Task<string> LikePost(LikePostDTO likePostDTO);

    Task<string> CommentPost(CommentDTO commentDTO);
}
