namespace MagicVilla_VillaAPI.Models.User.Dto
{
    public class LoginResponseDto
    {
        public UserModel User { get; set; }
        public string Token {  get; set; }
    }
}
