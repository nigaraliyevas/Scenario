namespace Scenario.Application.Dtos.ContactUsDtos
{
    public class ContactUsUpdateDto
    {
        public string FullName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
