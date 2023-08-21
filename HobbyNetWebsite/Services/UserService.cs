﻿using HobbyNetWebsite.Models.Resused;
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
            return null;
        }
    }
}