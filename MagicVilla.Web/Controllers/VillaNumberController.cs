using AutoMapper;
using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Models.VillaNumber;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;
        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;   
        }

        public async Task<IActionResult> Index()
        {
            List<VillaNumberDto> data = new List<VillaNumberDto>();
            var res = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<VillaNumberDto>>(res.Result.ToString());
            }
            return View(data);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            var res = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            var villa = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            ViewBag.ListVilla = villa.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _villaNumberService.CreateAsync<APIResponse>(dto, HttpContext.Session.GetString(SD.SessionToken));
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            var res = await _villaNumberService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(SD.SessionToken));
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
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            var res = await _villaNumberService.DeleteAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            return RedirectToAction(nameof(Index));
        }
    }
}
