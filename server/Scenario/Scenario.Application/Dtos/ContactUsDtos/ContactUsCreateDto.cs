namespace Scenario.Application.Dtos.ContactUsDtos
{
    public class ContactUsCreateDto
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
