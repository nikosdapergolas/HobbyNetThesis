using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.AuthService
{
    public interface IAuthService
    {
        public User Register(UserSignUpDTO request);
        public string Login(UserLoginDTO request);
        public string createToken(User user);
    }
}
