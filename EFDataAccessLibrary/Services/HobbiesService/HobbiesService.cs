using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        var hobbies = await _context.Hobbies
            .ToListAsync();
        return hobbies;
    }

    public async Task<IEnumerable<Hobby>> SearchHobby(string searchTerm)
    {
        var hobbies = await _context.Hobbies
            .Where(s => s.hobbyName.Contains(searchTerm))
            .ToListAsync();
        return hobbies;
    }

    public async Task<Hobby> GetOneHobby(int id)
    {
        var hobby = await _context.Hobbies.FindAsync(id);
        return hobby;
    }

    public async Task<Hobby> CreateHobby(Hobby hobby)
    {
        throw new NotImplementedException();
    }

    public async Task<string> EditHobby(Hobby hobby)
    {
        throw new NotImplementedException();
    }

    public async Task<string> DeleteHobby(int id)
    {
        throw new NotImplementedException();
    }
}
