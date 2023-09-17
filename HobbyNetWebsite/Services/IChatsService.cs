using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IChatsService
{
    Task<string> CreateANewChatMessage(CreateNewMessageInChatDTO newMessageInChatDTO);
    Task<List<Chat>> GetAllChatsOfOneUser(string username);
    Task<Chat> GetConversationBetweenTwoUsers(GetConversationOfTwoUsers twoUsers);
}