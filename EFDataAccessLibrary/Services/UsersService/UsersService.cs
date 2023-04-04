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
        IEnumerable<User> users = new List<User>();
        users = _context.Users;
        return users;
    }

    public async Task<IEnumerable<string>> GetOneUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> CreateAdminUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> EditUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> DeleteUser()
    {
        throw new NotImplementedException();
    }

    //public async Task<IEnumerable<User>> GetAllUsers()
    //{
    //    var users = _context.Users.AsAsyncEnumerable();
    //    return (IEnumerable<User>)users;
    //}

    //public async Task<IEnumerable<string>> GetOneUser(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<IEnumerable<string>> CreateAdminUser()
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<IEnumerable<string>> EditUser()
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<IEnumerable<string>> DeleteUser()
    //{
    //    throw new NotImplementedException();
    //}
}
