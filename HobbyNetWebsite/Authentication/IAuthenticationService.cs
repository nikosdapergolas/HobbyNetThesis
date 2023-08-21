using HobbyNetWebsite.Models;

namespace HobbyNetWebsite.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(SignInModel userForAuthentication);
    Task Logout();
    Task<string> Register(RegisterUserModel registerUserModel);
}
