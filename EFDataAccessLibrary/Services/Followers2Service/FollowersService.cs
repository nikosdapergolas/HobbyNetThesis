﻿using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.Followers2Service;

public class FollowersService : IFollowersService
{
    private readonly DatabaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FollowersService(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Followers>> GetAllFollowers()
    {
        try
        {
            var followers = await _context.Followers
                .ToListAsync();
            return followers;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<string>> GetFollowersOfOnePerson(string username)
    {
        try
        {
            // First checking if user exists
            var user = await _context.Users
                .Where(u => u.username == username)
                .FirstOrDefaultAsync();

            if (user is null) 
            {
                return null!;
            }

            // Then checking for this user's followers
            var followers = await _context.Followers
                .Where(f => f.FollowerUsername.Trim() == username)
                .Select(f => f.FolloweeUsername)
                .ToListAsync();

            // Removing all white spaces
            for (int i = 0; i < followers.Count(); i++)
            {
                followers[i] = followers[i].Trim();
            }

            // Returning the list of followers if he has any
            if(followers.Count > 0)
            {
                return followers;
            }
            else
            {
                return new List<string>();
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<User>> GetFollowersOfOnePersonAsUsers(string username)
    {
        try
        {
            // First checking if user exists
            var user = await _context.Users
                .Where(u => u.username == username)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null!;
            }

            List<User> followersOfUser = new();

            // Then checking for this user's followers
            var followers = await _context.Followers
                .Where(f => f.FolloweeUsername.Trim() == username)
                .Select(f => f.FollowerUsername)
                .ToListAsync();

            if(followers is not null)
            {
                foreach (var follower in followers)
                {
                    followersOfUser.Add(
                        await _context.Users
                        .Where(u => u.username.Equals(follower))
                        .FirstOrDefaultAsync()
                    );
                }
            }           

            //// Removing all white spaces
            //for (int i = 0; i < followers.Count(); i++)
            //{
            //    followers[i] = followers[i].Trim();
            //}

            // Returning the list of followers if he has any
            if (followersOfUser.Count > 0)
            {
                return followersOfUser;
            }
            else
            {
                return new List<User>();
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<string>> GetAllPeopleOneUserFollows(string username)
    {
        try
        {
            // First checking if user exists
            var user = await _context.Users
                .Where(u => u.username == username)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null!;
            }

            // Then checking for this user's followers
            var followers = await _context.Followers
                .Where(f => f.FolloweeUsername.Trim() == username)
                .Select(f => f.FollowerUsername)
                .ToListAsync();

            // Removing all white spaces
            for (int i = 0; i < followers.Count(); i++)
            {
                followers[i] = followers[i].Trim();
            }

            // Returning the list of followers if he has any
            if (followers.Count > 0)
            {
                return followers;
            }
            else
            {
                return new List<string>();
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<Followers> FollowAPerson(FollowersDTO followersDTO)
    {
        //followersDTO.FollowerUsername = followersDTO.FollowerUsername.Trim();
        //followersDTO.FolloweeUsername = followersDTO.FolloweeUsername.Trim();

        try
        {
            // Firstly Checking to see that the user does not try to follow himself
            if (followersDTO.FolloweeUsername == followersDTO.FollowerUsername)
            {
                return null!;
            }

            // Secondly checking if user exists
            var user = await _context.Users
                .Where(u => u.username == followersDTO.FollowerUsername)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null!;
            }

            // Thirdly checking if the Logged in user already follows this person
            var follow = await _context.Followers
                .Where(ee => ee.FolloweeUsername == followersDTO.FolloweeUsername)
                .Where(er => er.FollowerUsername == followersDTO.FollowerUsername)
                .FirstOrDefaultAsync();

            if (follow is not null)
            {
                return null!;
            }

            // Procceed to follow that person
            Followers newFollower = new();
            newFollower.FollowerUsername = followersDTO.FollowerUsername;
            newFollower.FolloweeUsername = followersDTO.FolloweeUsername;

            await _context.AddAsync(newFollower);
            await _context.SaveChangesAsync();

            return newFollower;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }

    public async Task<string> UnfollowAPerson(FollowersDTO followersDTO)
    {
        try
        {
            // Firstly Checking to see that the user does not try to unfollow himself
            if (followersDTO.FolloweeUsername == followersDTO.FollowerUsername)
            {
                return null!;
            }

            // Secondly checking if user exists
            var user = await _context.Users
                .Where(u => u.username == followersDTO.FollowerUsername)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null!;
            }

            // Thirdly checking if the Logged in user already follows this person
            var follow = await _context.Followers
                .Where(ee => ee.FolloweeUsername == followersDTO.FolloweeUsername)
                .Where(er => er.FollowerUsername == followersDTO.FollowerUsername)
                .FirstOrDefaultAsync();

            if (follow is null)
            {
                return null!;
            }

            // Procceed to unfollow that person
            await _context.Followers
                .Where(ee => ee.FolloweeUsername == followersDTO.FolloweeUsername)
                .Where(er => er.FollowerUsername == followersDTO.FollowerUsername)
                .ExecuteDeleteAsync();

            return $"user {followersDTO.FollowerUsername} has been successfully been unfollowed";
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null!;
        }
    }
}
