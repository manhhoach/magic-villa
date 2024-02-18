using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.User;
using MagicVilla_VillaAPI.Models.User.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository.User
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly string SecretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration config) : base(db)
        {
            _db = db;
            SecretKey = config.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = user, Token = tokenHandler.WriteToken(token)
            };
            return loginResponseDto;
        }

        public async Task<UserModel> Register(RegisterDto registerDto)
        {
            UserModel user = new UserModel()
            {
                Name = registerDto.Name,
                UserName = registerDto.UserName,
                Role = registerDto.Role,
                Password = registerDto.Password,
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
