using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebsite.Services;

public class PostsService : IPostsService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public PostsService(IConfiguration config, HttpClient client)
    {
        _config = config;
        _client = client;
    }

    public async Task<List<Post>> GetPosts()
    {
        string postsEndpoint = _config["getAllPostsEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Post>>(postsEndpoint);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Post> GetOnePost(int postId)
    {
        string postEndpoint = _config["getOnePostEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<Post>(postEndpoint + "/" + postId);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<List<Post>> GetPostsFromOneUser(string username)
    {
        string postsEndpoint = _config["getPostsFromOneUser"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Post>>(postsEndpoint + "/" + username);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<List<Post>> GetPostsFromOneHobby(string hobby)
    {
        string postsEndpoint = _config["getPostsFromOneHobby"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Post>>(postsEndpoint + "/" + hobby);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<List<Post>> GetSomePosts(int currentPage)
    {
        string postsEndpoint = _config["getSomePostsEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Post>>(postsEndpoint + "/" + currentPage);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<List<Post>> GetSomePostsFromOneHobby(int currentPage, string hobby)
    {
        string postsEndpoint = _config["getPeginatedPostsFromOneHobby"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Post>>(postsEndpoint + "/" + hobby + "/" + currentPage);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<string> CreatePost(PostCreateDTO postCreateDTO)
    {
        string createPostEndpoint = _config["createPostEndpoint"];

        try
        {
            var result = await _client.PostAsJsonAsync<PostCreateDTO>(createPostEndpoint, postCreateDTO);
            return "Ok";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<string> DeletePost(int postId)
    {
        string deletePostEndpoint = _config["deletePostEndpoint"];

        try
        {
            var result = await _client.DeleteAsync(deletePostEndpoint + "/" + postId);

            return result.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> EditPost(PostEditDTO postEditDTO)
    {
        string editPostEndpoint = _config["deletePostEndpoint"];

        try
        {
            var result = await _client.PutAsJsonAsync(editPostEndpoint, postEditDTO);

            return result.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> LikePost(LikePostDTO likePostDTO)
    {
        string likePostEndpoint = _config["likePostEndpoint"];

        try
        {
            var result = await _client.PostAsJsonAsync(likePostEndpoint, likePostDTO);

            return result.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> CommentPost(CommentDTO commentDTO)
    {
        string commentPostEndpoint = _config["commentPostEndpoint"];

        try
        {
            var result = await _client.PostAsJsonAsync(commentPostEndpoint, commentDTO);

            return result.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}
