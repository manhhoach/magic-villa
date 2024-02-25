using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Services.IServices;

namespace MagicVilla.Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string URL_API;
        private string URL_VILLA = "/api/v1/villaAPI/";
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            URL_API = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.POST, Data = dto, Url = URL_API + URL_VILLA, Token = token });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.DELETE, Url = URL_API + URL_VILLA + id, Token = token });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = URL_API + URL_VILLA, Token = token });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = URL_API + URL_VILLA + id, Token = token });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.PUT, Data = dto, Url = URL_API + URL_VILLA + dto.Id, Token = token });
        }
    }
}
