using AutoMapper;
using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> data = new List<VillaDTO>();
            var res = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            }
            return View(data);
        }
    }
}
