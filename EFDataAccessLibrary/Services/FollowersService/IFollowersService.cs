using EFDataAccessLibrary.Models;

namespace EFDataAccessLibrary.Services.FollowersService;

public interface IFollowersService
{
    public Task<IEnumerable<Followers>> GetAllFollowers();
}