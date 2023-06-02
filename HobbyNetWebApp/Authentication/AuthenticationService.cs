using Blazored.LocalStorage;
using HobbyNetWebApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HobbyNetWebApp.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public AuthenticationService(HttpClient client,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorageService)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
    }

    public async Task<string> Login(AuthenticationUserModel userForAuthentication)
    {
        var authResult = await _client.PostAsJsonAsync("/api/Auth/login", userForAuthentication);

        var result = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode == false)
        {
            return null;
        }

        await _localStorageService.SetItemAsync("authToken", result);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Replace("\"", ""));

        return result;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        // Casting it to the right type (To the class that i made that extends AuthenticationStateProvider)
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}
