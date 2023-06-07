using HobbyNetWebApp.Models;
using HobbyNetWebApp.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebApp.Services;

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
}
