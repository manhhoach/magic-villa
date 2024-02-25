using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla.Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClientFactory { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            responseModel = new();
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MagicAPI");
                HttpRequestMessage m = new HttpRequestMessage();
                m.Headers.Add("Accept", "application/json");
                m.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    m.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.APIType)
                {
                    case SD.APIType.GET:
                        m.Method = HttpMethod.Get;
                        break;
                    case SD.APIType.POST:
                        m.Method = HttpMethod.Post;
                        break;
                    case SD.APIType.DELETE:
                        m.Method = HttpMethod.Delete;
                        break;
                    case SD.APIType.PUT:
                        m.Method = HttpMethod.Put;
                        break;
                }
                HttpResponseMessage apiResponse = null;

                if(!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(m);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<T>(apiContent);
                return res;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorMessages = new List<string>() { ex.Message },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiRes = JsonConvert.DeserializeObject<T>(res);
                return apiRes;
            }

        }
    }
}
