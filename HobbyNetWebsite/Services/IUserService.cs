using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();

    public Task<User> GetOneUserByUsername(string userName);

    Task<string> EditUserProfile(UserEditDTO userEditDTO);
}
