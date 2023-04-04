using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
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
        var user = await GetOneUser(id);
        if(user == null)
        {
            return null;
        }
        else
        {
            //Role role = new Role();
            //role.roleName = "Admin";
            //_context.Roles.Add("Admin", id);
            _context.Roles.FromSqlRaw($"spUpgradeUserToAdmin {id}");
            _context.SaveChangesAsync();
            return $"User with id {id} has been granted Admin privileges";
        }
    }

    public async Task<IEnumerable<string>> EditUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> DeleteUser()
    {
        throw new NotImplementedException();
    }
}
