using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.User;
using MagicVilla_VillaAPI.Models.User.Dto;

namespace MagicVilla_VillaAPI.Repository.User
{
    public interface IUserRepository: IRepository<UserModel>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<UserModel> Register(RegisterDto registerDto);
    }
}
