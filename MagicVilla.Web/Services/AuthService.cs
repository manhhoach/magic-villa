using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.User;
using MagicVilla.Web.Services.IServices;

namespace MagicVilla.Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        private string route = "/api/user/";
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginDto loginDto)
        {
            return SendAsync<T>(new APIRequest
            {
                APIType = SD.APIType.POST,
                Data = loginDto,
                Url = villaUrl + "login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterDto registerDto)
        {
            return SendAsync<T>(new APIRequest
            {
                APIType = SD.APIType.POST,
                Data = registerDto,
                Url = villaUrl + "register"
            });
        }
    }
}
