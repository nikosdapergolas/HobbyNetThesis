using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.HobbiesService;

public class HobbiesService : IHobbiesService
{
    private readonly DatabaseContext _context;

    public HobbiesService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hobby>> GetAllHobbies()
    {
        try
        {
            var hobbies = await _context.Hobbies
                .ToListAsync();
            return hobbies;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Hobby>> SearchHobby(string searchTerm)
    {
        try
        {
            var hobbies = await _context.Hobbies
                .Where(s => s.hobbyName.Contains(searchTerm))
                .ToListAsync();
            return hobbies;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Hobby> GetOneHobby(int id)
    {
        try
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            return hobby;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return null;
        }
    }

    public async Task<Hobby> CreateHobby(Hobby hobby)
    {
        var hobbyFound = await _context.Hobbies
            .Where(name => name.hobbyName.Equals(hobby.hobbyName))
            .FirstOrDefaultAsync();

        if (hobbyFound is null)
        {
            try
            {
                await _context.AddAsync(hobby);
                await _context.SaveChangesAsync();

                return hobby;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public async Task<string> EditHobby(Hobby hobby)
    {
        var hobbyToEdit = await _context.Hobbies.FindAsync(hobby.Id);

        if (hobbyToEdit is null)
        {
            return null;
        }
        else
        {
            try
            {
                await _context.Hobbies
                    .Where(i => i.Id.Equals(hobby.Id))
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(name => name.hobbyName, name => hobby.hobbyName)
                );

                return $"Hobby with Id: {hobby.Id} has been edited successfully";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }

    public async Task<string> DeleteHobby(int id)
    {
        var hobbyToDelete = await _context.Hobbies.FindAsync(id);

        if (hobbyToDelete is null)
        {
            return null;
        }
        else
        {
            try
            {
                await _context.Hobbies
                    .Where(i => i.Id.Equals(id))
                    .ExecuteDeleteAsync();

                return $"Hobby with Id: {id} has been deleted successfully";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }
}
