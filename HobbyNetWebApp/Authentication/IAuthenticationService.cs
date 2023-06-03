using HobbyNetWebApp.Models;

namespace HobbyNetWebApp.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(AuthenticationUserModel userForAuthentication);
    Task Logout();
    Task<string> Register(RegisterUserModel registerUserModel);
}
