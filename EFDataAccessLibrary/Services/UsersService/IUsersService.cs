using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.UsersService;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<IEnumerable<string>> GetOneUser(int id);
    public Task<IEnumerable<string>> CreateAdminUser();
    public Task<IEnumerable<string>> EditUser();
    public Task<IEnumerable<string>> DeleteUser();

}
