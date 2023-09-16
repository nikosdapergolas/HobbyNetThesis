using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;

namespace EFDataAccessLibrary.Services.ChatsService
{
    public interface IChatsService
    {
        Task<IEnumerable<Chat>> GetAllChats();
        Task<IEnumerable<Chat>> GetAllChatsOfOneUser(string username);
        Task<Chat> CreateANewChat(CreateNewMessageInChatDTO newMessageInChatDTO);
    }
}