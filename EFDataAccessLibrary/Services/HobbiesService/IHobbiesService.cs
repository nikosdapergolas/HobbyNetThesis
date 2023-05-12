using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.HobbiesService;

public interface IHobbiesService
{
    public Task<IEnumerable<Hobby>> GetAllHobbies();
    public Task<IEnumerable<Hobby>> SearchHobby(string searchTerm);
    public Task<Hobby> GetOneHobby(int id);
    public Task<Hobby> CreateHobby(Hobby hobby);
    public Task<string> EditHobby(Hobby hobby);
    public Task<string> DeleteHobby(int id);
}
