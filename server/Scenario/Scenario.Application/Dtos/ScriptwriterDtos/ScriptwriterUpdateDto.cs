namespace Scenario.Application.Dtos.ScriptwriterDtos
{
    public class ScriptwriterUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? About { get; set; }
        public DateTime? BirthDay { get; set; } = DateTime.UtcNow;

    }
}
