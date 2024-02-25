using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.VillaNumber;
using MagicVilla.Web.Services.IServices;

namespace MagicVilla.Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string URL_API;
        private string URL_VILLA_NUMBER = "/api/villaNumberAPI/";
        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            URL_API = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType=SD.APIType.POST, Data = dto, Url = URL_API+ URL_VILLA_NUMBER, Token = token });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.DELETE, Url = URL_API + URL_VILLA_NUMBER + id, Token = token });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = URL_API + URL_VILLA_NUMBER, Token = token });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = URL_API + URL_VILLA_NUMBER +id, Token = token });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.PUT, Data = dto, Url = URL_API + URL_VILLA_NUMBER + dto.VillaId, Token = token });
        }
    }
}
