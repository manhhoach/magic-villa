using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.User;
using MagicVilla_VillaAPI.Models.User.Dto;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IConfiguration config, UserManager<AppUser> userManager, IMapper mapper) : base(db)
        {
            _db = db;
            SecretKey = config.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.AppUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = await _db.AppUsers.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
            {
                return null;
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
           if(!isValid)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = _mapper.Map<UserDto>(user), Token = tokenHandler.WriteToken(token), Role = role
            };
            return loginResponseDto;
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            AppUser user = new AppUser()
            {
                Name = registerDto.Name,
                UserName = registerDto.UserName,
                Email = registerDto.UserName,
                NormalizedEmail = registerDto.UserName.ToUpper(),
            };
            try
            {
                var res = await _userManager.CreateAsync(user, registerDto.Password);
                if(res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registerDto.Role);
                    var userReturned = _db.AppUsers.FirstOrDefault(u=>u.UserName == registerDto.UserName);
                    return _mapper.Map<UserDto>(userReturned);
                }
            }
            catch(Exception ex) { 
            }
            return new UserDto() { };
        }
    }
}
