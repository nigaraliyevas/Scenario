namespace Scenario.Application.Dtos.ScriptwriterDtos
{
    public class ScriptwriterCreateDto
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? About { get; set; }
        public DateTime? BirthDay { get; set; } = DateTime.UtcNow;
    }
}
