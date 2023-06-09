﻿using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public AuthService(DatabaseContext context, IConfiguration configuration, ILogger<User> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }
    public User Register(UserSignUpDTO request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        Role role = new Role();
        role.roleName = "User";

        User user = new User();
        user.username = request.Username;
        user.passwordHash = passwordHash;
        user.email = request.Email;
        user.firstname = request.Firstname;
        user.lastname = request.Lastname;
        user.profileImage = "/images/default.jpg";
        user.Roles.Add(role);

        _logger.LogInformation("User tries to register using the following info: {username},{passwordHash},{email},{firstname},{lastname}"
            ,user.username
            ,user.passwordHash
            ,user.email
            ,user.firstname
            ,user.lastname);

        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

        return user;
    }
    public string Login(UserLoginDTO request)
    {
        // Searching the DB to find a user with this username
        User? user = _context.Users
            .Where( u => u.username.Equals(request.Username))
            .FirstOrDefault();

        // Checking to see if user Exists
        if (user is null) 
        {
            return "Not found";
        }
        else
        {

            //Checking to see if password exists
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.passwordHash))
            {
                return "Not found";
            }

            // Creating the Jason Web Token For this user
            string token = createToken(user);

            return token;
        }
    }
    public string createToken(User user)
    {
        //var roles = _context.Roles.Where(r => r.Id.Equals(user.Id)).ToList();
        var roles = _context.Roles.FromSqlRaw($"spCheckUserRoles {user.Id}").ToList();
        //var roles = _context.Users.Where(r => r.Id.Equals(user.Id)).Include(u => u.Roles.Find(i => i.roleName));
        //var roles = _context.Users.Where(u => u.Id.Equals(user.Id)).Include(i => i.Roles).ToList();

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.GivenName, user.username),
            new Claim(ClaimTypes.Name, user.firstname),
            new Claim(ClaimTypes.Surname, user.lastname),
            new Claim(ClaimTypes.Email, user.email)
        };
        // Adding all the roles of this user in his claim
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.roleName));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials
            );

        var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jsonWebToken;
    }
}
