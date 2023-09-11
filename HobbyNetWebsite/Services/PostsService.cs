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
}
