namespace Scenario.Application.Dtos.UserDtos
{
    public class ExternalLoginDto
    {
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string Email { get; set; }
    }
}
