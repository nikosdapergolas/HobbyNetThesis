using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
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
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Hobby>> SearchHobby(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Hobby> GetOneHobby(int id)
    {
        throw new NotImplementedException();
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
