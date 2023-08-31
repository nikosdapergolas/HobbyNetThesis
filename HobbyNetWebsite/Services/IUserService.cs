using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();

    public Task<User> GetOneUserByUsername(string userName);
}
