using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebsite.Services;

public class HobbiesService : IHobbiesService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public HobbiesService(IConfiguration config, HttpClient client)
    {
        _config = config;
        _client = client;
    }

    public async Task<IEnumerable<Hobby>> GetAllHobbies()
    {
        string hobbiesEndpoint = _config["getAllHobbies"];

        try
        {
            string requestLink = hobbiesEndpoint;
            var result = await _client.GetFromJsonAsync<List<Hobby>>(requestLink);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> FollowAHobby(FollowHobbyDTO followHobbyDTO)
    {
        string followHobbyEndpoint = _config["followOneHobby"];

        try
        {
            var result = await _client.PostAsJsonAsync(followHobbyEndpoint, followHobbyDTO);
            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> UnfollowAHobby(FollowHobbyDTO followHobbyDTO)
    {
        string unfollowHobbyEndpoint = _config["unfollowOneHobby"];

        try
        {
            //string requestLink = followHobbyEndpoint;
            var result = await _client.PostAsJsonAsync(unfollowHobbyEndpoint, followHobbyDTO);
            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> CreateNewHobby(Hobby hobby)
    {
        string createNewHobbyEndpoint = _config["createNewHobbyByAdmin"];

        try
        {
            var result = await _client.PostAsJsonAsync(createNewHobbyEndpoint, hobby);
            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
