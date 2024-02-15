using AutoMapper;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Models.VillaNumber;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaNumberDto> data = new List<VillaNumberDto>();
            var res = await _villaNumberService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<VillaNumberDto>>(res.Result.ToString());
            }
            return View(data);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _villaNumberService.CreateAsync<APIResponse>(dto);
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

        public async Task<IActionResult> UpdateVillaNumber(int id)
        {
            var res = await _villaNumberService.GetAsync<APIResponse>(id);
            if (res != null && res.IsSuccess)
            {
                VillaDTO data = ConvertFromResponse(res);
                return View(_mapper.Map<VillaUpdateDTO>(data));
            }

            return NotFound();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVillaNumber(VillaUpdateDTO dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var res = await _villaNumberService.UpdateAsync<APIResponse>(dto);
        //        if (res != null && res.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(dto);
        //}

        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            var res = await _villaNumberService.DeleteAsync<APIResponse>(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
