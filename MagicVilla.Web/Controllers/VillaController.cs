using AutoMapper;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> data = new List<VillaDTO>();
            var res = await _villaService.GetAllAsync<APIResponse>();
            if(res!=null&&res.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            }
            return View(data);
        }
    }
}
