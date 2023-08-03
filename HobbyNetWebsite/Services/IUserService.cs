using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
}
