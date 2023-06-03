using HobbyNetWebApp.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebApp.Services;

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
        string api = _config["getAllUsersEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<User>>(api);

            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }
}
