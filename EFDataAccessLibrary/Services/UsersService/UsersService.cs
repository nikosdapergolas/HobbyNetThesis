using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly DatabaseContext _context;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersService(
        DatabaseContext context,
        IConfiguration config,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            var users = await _context.Users
            .Include(h => h.Hobbies)
            .Include(r => r.Roles)
            .ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
        
    }

    public async Task<IEnumerable<UserSearchResultDTO>> SearchUsers(string searchTerm)
    {
        try
        {
            var users = await _context.Users
            .Where(u => u.username.Contains(searchTerm))
            .Select(x => new UserSearchResultDTO()
            {
                Id = x.Id,
                username = x.username
            })
            .ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<User> GetOneUserByUsername(string username)
    {
        try
        {
            var user = await _context.Users
                .Where(i => i.username.Equals(username))
                .Include(h => h.Hobbies)
                .Include(r => r.Roles)
                .FirstOrDefaultAsync();
            return user;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<User> GetOneUser(int id)
    {
        try
        {
            var user = await _context.Users
                .Where(i => i.Id.Equals(id))
                .Include(h => h.Hobbies)
                .Include(r => r.Roles)
                .FirstOrDefaultAsync();
            return user;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<string> CreateAdminUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }
        else
        {
            try
            {
                Role role = new Role();
                role.roleName = "Admin";
                role.UserId = id;

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                return $"User with id {id} has been granted Admin privileges";
                //_context.Roles.FromSqlRaw($"spUpgradeUserToAdmin {id}");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<string> EditUser(UserEditDTO userEditDTO)
    {
        //var user = await _context.Users.FindAsync(userEditDTO.Id);
        var user = await _context.Users
            .Where(u => u.username.Equals(userEditDTO.username))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return null;
        }
        else
        {
            try
            {
                await _context.Users
                    .Where(u => u.username.Equals(userEditDTO.username))
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(f => f.firstname, f => userEditDTO.firstname)
                        .SetProperty(l => l.lastname, f => userEditDTO.lastname)
                        .SetProperty(e => e.email, f => userEditDTO.email)
                        .SetProperty(u => u.username, u => userEditDTO.username)
                    );

                return $"User with username {userEditDTO.username} has been edited Successfully";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<string> DeleteUser(int id)
    {
        //var user = await GetOneUser(id);
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }
        else
        {
            try
            {
                await _context.Users
                    .Where(i => i.Id.Equals(id))
                    .ExecuteDeleteAsync();

                return $"User with id {id} has been Deleted Successfully";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<UploadResult> UploadFile(IFormFile file)
    {
        string username = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.GivenName);
        UploadResult uploadResult = new();
        uploadResult.FileName = file.Name;

        string newFileName = Path.ChangeExtension(
            Path.GetRandomFileName(),
            Path.GetExtension(file.FileName));

        var path = Path.Combine(
            _config.GetValue<string>("FileStorage"),
            username +
            //_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.GivenName) + // This is the logged in user's username
            "_" +
            newFileName);

        await using FileStream fs = new(path, FileMode.Create);
        await file.CopyToAsync(fs);

        uploadResult.StoredFileName = newFileName;

        await _context.Users
                    .Where(u => u.username.Equals(username))
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(
                            pi => pi.profileImage, 
                            pi => _config.GetValue<string>(
                                    "onlineFileStorage") + 
                                    username + 
                                    "_" + 
                                    newFileName)
                    );

        return uploadResult;
    }
}
