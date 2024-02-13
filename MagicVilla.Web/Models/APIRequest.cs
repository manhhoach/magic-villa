using Microsoft.AspNetCore.Mvc;
using static MagicVilla.Utility.SD;

namespace MagicVilla.Web.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string Url { get; set; } 
        public object Data { get; set; }
    }
}
