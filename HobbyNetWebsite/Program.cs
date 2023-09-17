using Blazored.LocalStorage;
using HobbyNetWebsite;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using HobbyNetWebsite.Authentication;
using HobbyNetWebsite.Services;
using HobbyNetWebsite.Models;
using HobbyNetWebsite.Models.Resused;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//----------------------------------------------------------------------------------------------------------------
// My own Dependency Injection
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IFollowersService, FollowersService>();
builder.Services.AddScoped<IHobbiesService, HobbiesService>();
builder.Services.AddScoped<IChatsService, ChatsService>();
//----------------------------------------------------------------------------------------------------------------

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7213/") });

await builder.Build().RunAsync();
