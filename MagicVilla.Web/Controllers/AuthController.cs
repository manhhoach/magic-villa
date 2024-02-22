using MagicVilla.Web.Models;
using MagicVilla.Web.Models.User;
using MagicVilla.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
            
            return View();
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
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
