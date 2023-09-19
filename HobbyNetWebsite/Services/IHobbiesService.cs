using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

namespace HobbyNetWebsite.Services;

public interface IHobbiesService
{
    Task<IEnumerable<Hobby>> GetAllHobbies();

    Task<string> FollowAHobby(FollowHobbyDTO followHobbyDTO);

    Task<string> UnfollowAHobby(FollowHobbyDTO followHobbyDTO);

    Task<string> CreateNewHobby(Hobby hobby);
}