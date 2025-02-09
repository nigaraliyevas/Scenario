namespace Scenario.Application.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string? Image { get; set; }
    }
}
