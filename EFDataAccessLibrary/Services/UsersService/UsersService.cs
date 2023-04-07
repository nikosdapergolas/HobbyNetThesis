using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly DatabaseContext _context;

    public UsersService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _context.Users
            .Include(h => h.Hobbies)
            .Include(r => r.Roles)
            .ToListAsync();
        return users;
    }

    public async Task<IEnumerable<UserSearchResultDTO>> SearchUsers(string searchTerm)
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

    public async Task<User> GetOneUser(int id)
    {
        var user = await _context.Users
            .Where(i => i.Id.Equals(id))
            .Include(h => h.Hobbies)
            .Include(r => r.Roles)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<string> CreateAdminUser(int id)
    {
        //var user = await GetOneUser(id);
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }
        else
        {
            Role role = new Role();
            role.roleName = "Admin";
            role.UserId = id;

            await _context.Roles.AddAsync(role);            
            await _context.SaveChangesAsync();

            return $"User with id {id} has been granted Admin privileges";
            //_context.Roles.FromSqlRaw($"spUpgradeUserToAdmin {id}");
        }
    }

    public async Task<string> EditUser(UserEditDTO userEditDTO)
    {
        //var user = await GetOneUser(userEditDTO.Id);
        var user = await _context.Users.FindAsync(userEditDTO.Id);

        if (user == null)
        {
            return null;
        }
        else
        {
            await _context.Users
                .Where(i => i.Id.Equals(userEditDTO.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(f => f.firstname, f => userEditDTO.firstname)
                    .SetProperty(l => l.lastname, f => userEditDTO.lastname)
                    .SetProperty(e => e.email, f => userEditDTO.email)
                    .SetProperty(u => u.username, u => userEditDTO.username)
                );

            return $"User with id {userEditDTO.Id} has been edited Successfully";
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
            await _context.Users
                .Where(i => i.Id.Equals(id))
                .ExecuteDeleteAsync();

            return $"User with id {id} has been Deleted Successfully";
        }
    }
}
