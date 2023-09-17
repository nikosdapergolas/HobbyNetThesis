using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IFollowersService
{
    Task<IEnumerable<string>> GetFollowersOfOnePerson(string username);
    Task<IEnumerable<string>> GetAllPeopleOneUserFollows(string username);
    Task<string> UnfollowAPerson(FollowersDTO followersDTO);
    Task<string> FollowAPerson(FollowersDTO followersDTO);
    Task<IEnumerable<User>> GetFollowersOfOnePersonAsUsers(string username);
}