using EFDataAccessLibrary.Models;

namespace EFDataAccessLibrary.Services.Followers2Service;

public interface IFollowersService
{
    Task<IEnumerable<Followers>> GetAllFollowers();
}