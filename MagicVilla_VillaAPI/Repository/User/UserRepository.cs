using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.User;
using MagicVilla_VillaAPI.Models.User.Dto;

namespace MagicVilla_VillaAPI.Repository.User
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        private readonly ApplicationDbContext _db
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
