using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;
using System.Net.Http.Json;

namespace HobbyNetWebsite.Services;

public class ChatsService : IChatsService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public ChatsService(IConfiguration config, HttpClient client)
    {
        _config = config;
        _client = client;
    }

    public async Task<List<Chat>> GetAllChatsOfOneUser(string username)
    {
        string chatsEndpoint = _config["getAllChatsOfOneUserEndpoint"];

        try
        {
            var result = await _client.GetFromJsonAsync<List<Chat>>(chatsEndpoint + "/" + username);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Chat> GetConversationBetweenTwoUsers(GetConversationOfTwoUsers twoUsers)
    {
        string chatEndpoint = _config["getConversationBetweenTwoUsers"];

        try
        {
            var result = await _client.GetFromJsonAsync<Chat>(chatEndpoint + "/" + twoUsers.User1.username + "/" + twoUsers.User2.username);
            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<string> CreateANewChatMessage(CreateNewMessageInChatDTO newMessageInChatDTO)
    {
        string createChatMessageEndpoint = _config["createANewChatMessageEndpoint"];

        try
        {
            var result = await _client.PostAsJsonAsync<CreateNewMessageInChatDTO>(createChatMessageEndpoint, newMessageInChatDTO);
            return result.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
