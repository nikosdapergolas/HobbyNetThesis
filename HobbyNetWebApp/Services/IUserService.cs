using HobbyNetWebApp.Models.Resused;

namespace HobbyNetWebApp.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
}
