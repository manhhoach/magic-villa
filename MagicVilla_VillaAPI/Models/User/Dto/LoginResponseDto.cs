namespace MagicVilla_VillaAPI.Models.User.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token {  get; set; }
        public string Role { get; set; }
    }
}
