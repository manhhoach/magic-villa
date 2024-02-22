namespace MagicVilla.Web.Models.User
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token {  get; set; }
    }
}
