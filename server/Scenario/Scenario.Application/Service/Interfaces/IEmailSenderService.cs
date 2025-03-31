namespace Scenario.Application.Service.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}
