using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.FollowersService;

public class FollowersService : IFollowersService
{
    private readonly DatabaseContext _context;

    public FollowersService(DatabaseContext context)
    {
        _context = context;
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
            return null;
        }

    }
}
