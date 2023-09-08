using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebsite.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public UserService(IConfiguration config,
                       HttpClient client)
    {
        _config = config;
        _client = client;
    }
    public async Task<List<User>> GetUsers()
    {
        string usersEndpoint = _config["getAllUsersEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<User>>(usersEndpoint);

            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<User> GetOneUserByUsername(string userName)
    {
        string usersEndpoint = _config["getOneUserByUsername"];

        try
        {
            var result = await _client.GetFromJsonAsync<User>(usersEndpoint + "/" + userName);

            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> EditUserProfile(UserEditDTO userEditDTO)
    {
        string userEditEndpoint = _config["editUserProfile"];

        try
        {
            var result = await _client.PutAsJsonAsync(userEditEndpoint,userEditDTO);

            return result.ToString();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
