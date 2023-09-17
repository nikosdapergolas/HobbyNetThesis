using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;

namespace EFDataAccessLibrary.Services.Followers2Service;

public interface IFollowersService
{
    Task<IEnumerable<Followers>> GetAllFollowers();
    Task<IEnumerable<string>> GetFollowersOfOnePerson(string username);
    Task<Followers> FollowAPerson(FollowersDTO followersDTO);
    Task<string> UnfollowAPerson(FollowersDTO followersDTO);
    Task<IEnumerable<string>> GetAllPeopleOneUserFollows(string username);
    Task<IEnumerable<User>> GetFollowersOfOnePersonAsUsers(string username);
}