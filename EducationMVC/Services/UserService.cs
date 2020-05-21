using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MVCEdu.Data;
using MVCEdu.Helpers;
using MVCEdu.Models;
using SQLitePCL;

namespace MVCEdu.Services
{
    //AKO NE RABOTI ZARADI TOA KAKO GI PREVZEMAM PODATOCITE OD DATABAZATA E
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly MVCEduContext _context;

        public UserService(IOptions<AppSettings> appSettings, MVCEduContext context)
        {
            _appSettings = appSettings.Value;

            //prevzemanje na podatocite od DB
            _context = context;

        }


        public User Authenticate(string username, string password)
        {
            IQueryable<User> _users = _context.User.AsQueryable();

            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            IQueryable<User> _users = _context.User.AsQueryable();
            return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            IQueryable<User> _users = _context.User.AsQueryable();
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }

    }
}
