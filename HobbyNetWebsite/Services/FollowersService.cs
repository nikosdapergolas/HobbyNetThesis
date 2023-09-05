using HobbyNetWebsite.Models.Resused;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace HobbyNetWebsite.Services;

public class FollowersService : IFollowersService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;
    private string loggedInUser = string.Empty;

    public FollowersService(IConfiguration config, HttpClient client)
    {
        _config = config;
        _client = client;
    }

    public async Task<IEnumerable<string>> GetFollowersOfOnePerson(string username)
    {
        string followersEndpoint = _config["getAllFollowersOfOneUser"];

        try
        {
            string requestLink = followersEndpoint + "?username=" + username;
            var result = await _client.GetFromJsonAsync<List<string>>(requestLink);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<string>> GetAllPeopleOneUserFollows(string username)
    {
        string followersEndpoint = _config["GetAllPeopleOneUserFollows"];

        try
        {
            string requestLink = followersEndpoint + "?username=" + username;
            var result = await _client.GetFromJsonAsync<List<string>>(requestLink);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
