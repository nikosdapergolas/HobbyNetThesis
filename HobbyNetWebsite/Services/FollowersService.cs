using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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

    public async Task<string> UnfollowAPerson(FollowersDTO followersDTO)
    {
        string unfollowEndpoint = _config["unfollowAPerson"];

        // Serialize the DTO to JSON
        //string jsonContent = JsonSerializer.Serialize(followersDTO);

        // Create a StringContent with application/json content type
        //StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            //string requestLink = unfollowEndpoint + "/" + followersDTO;
            var result = await _client.PostAsJsonAsync(unfollowEndpoint, followersDTO);
            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> FollowAPerson(FollowersDTO followersDTO)
    {
        string followEndpoint = _config["followAPerson"];

        try
        {
            //string requestLink = followEndpoint + "/" + followersDTO;
            var result = await _client.PostAsJsonAsync(followEndpoint, followersDTO);
            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
