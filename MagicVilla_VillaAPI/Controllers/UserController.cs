using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.User.Dto;
using MagicVilla_VillaAPI.Repository.User;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _res;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _res = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginRes = await _userRepository.Login(loginDto);
            if (loginRes != null)
            {
                _res.IsSuccess = true;
                _res.StatusCode = System.Net.HttpStatusCode.OK;
                _res.Result = loginRes;
                return Ok(_res);
            }
            _res.IsSuccess = false;
            _res.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _res.ErrorMessages.Add("User name or password is incorrect");
            return BadRequest(_res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            bool isUnique =  _userRepository.IsUniqueUser(registerDto.UserName);
            if(isUnique)
            {
                var user = await _userRepository.Register(registerDto);
                if(user != null)
                {
                    _res.IsSuccess = true;
                    _res.StatusCode = System.Net.HttpStatusCode.Created;
                    return Ok(_res);

                }

            }
            _res.IsSuccess = false;
            _res.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _res.ErrorMessages = new List<string>() { "User name is already exists" };
            return BadRequest(_res);
        }
    }
}
