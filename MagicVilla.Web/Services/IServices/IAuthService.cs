using MagicVilla.Web.Models.User;

namespace MagicVilla.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginDto loginDto);
        Task<T> RegisterAsync<T>(RegisterDto registerDto);  
    }
}
