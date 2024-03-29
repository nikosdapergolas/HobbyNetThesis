﻿using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.UsersService;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<IEnumerable<UserSearchResultDTO>> SearchUsers(string searchTerm);
    public Task<User> GetOneUserByUsername(string username);
    public Task<User> GetOneUser(int id);
    public Task<string> CreateAdminUser(int id);
    public Task<string> EditUser(UserEditDTO userEditDTO);
    public Task<string> DeleteUser(int id);
    public Task<UploadResult> UploadFile(IFormFile file);
    //public Task<HobbiesOfUsers> FollowAHobby(FollowHobbyDTO followHobbyDTO);

}
