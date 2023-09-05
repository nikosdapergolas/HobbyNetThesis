using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IFollowersService
{
    Task<IEnumerable<string>> GetFollowersOfOnePerson(string username);
    Task<IEnumerable<string>> GetAllPeopleOneUserFollows(string username);
}