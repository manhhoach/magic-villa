using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.VillaNumber;
using MagicVilla.Web.Services.IServices;

namespace MagicVilla.Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        private string route = "/api/villaNumberAPI/";
        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto)
        {
            return SendAsync<T>(new APIRequest() { APIType=SD.APIType.POST, Data = dto, Url = villaUrl+ route });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.DELETE, Url = villaUrl + route + id });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = villaUrl + route });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = villaUrl + route +id });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.PUT, Data = dto, Url = villaUrl + route + dto.VillaId });
        }
    }
}
