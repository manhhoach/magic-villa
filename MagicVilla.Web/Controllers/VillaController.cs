using AutoMapper;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (res != null && res.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            }
            return View(data);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _villaService.CreateAsync<APIResponse>(dto);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dto);
        }

        private VillaDTO ConvertFromResponse(APIResponse res)
        {
            return JsonConvert.DeserializeObject<VillaDTO>(res.Result.ToString());
        }

        public async Task<IActionResult> UpdateVilla(int id)
        {
            var res = await _villaService.GetAsync<APIResponse>(id);
            if (res != null && res.IsSuccess)
            {
                VillaDTO data = ConvertFromResponse(res);
                return View(_mapper.Map<VillaUpdateDTO>(data));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _villaService.UpdateAsync<APIResponse>(dto);
                if (res != null && res.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> DeleteVilla(int id)
        {
            var res = await _villaService.DeleteAsync<APIResponse>(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
