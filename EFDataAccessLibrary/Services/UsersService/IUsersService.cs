using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.UsersService;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User> GetOneUser(int id);
    public Task<string> CreateAdminUser(int id);
    public Task<string> EditUser(UserEditDTO userEditDTO);
    public Task<string> DeleteUser(int id);

}
