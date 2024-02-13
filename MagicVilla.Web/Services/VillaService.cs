using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Services.IServices;

namespace MagicVilla.Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = config.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest() { APIType=SD.APIType.POST, Data = dto, Url = villaUrl+"/api/villaAPI" });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.DELETE, Url = villaUrl + "/api/villaAPI/"+id });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = villaUrl + "/api/villaAPI" });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.GET, Url = villaUrl + "/api/villaAPI/"+id });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest() { APIType = SD.APIType.PUT, Data = dto, Url = villaUrl + "/api/villaAPI/"+dto.Id });
        }
    }
}
