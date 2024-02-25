using MagicVilla.Utility;
using MagicVilla.Web.Models;
using MagicVilla.Web.Models.User;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MagicVilla.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) {
            _authService = authService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginDto dto = new LoginDto();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            APIResponse res = await _authService.LoginAsync<APIResponse>(dto);
            if (res != null && res.IsSuccess == true)
            {
                LoginResponseDto model = JsonConvert.DeserializeObject<LoginResponseDto>(res.Result.ToString());

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, model.User.Role));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Error", res.ErrorMessages.FirstOrDefault());
            return View(dto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterDto dto = new RegisterDto();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
           var data =  await _authService.RegisterAsync<APIResponse>(dto);
            if(data!=null&&data.IsSuccess)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
