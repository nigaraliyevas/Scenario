using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Scenario.Application.Exceptions;
using Scenario.Application.Service.Interfaces;
using Scenario.Application.Settings;
using System.Net;
using System.Net.Mail;

namespace Scenario.Application.Service.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly EmailConfigurationSettings _emailConfig;

        public EmailSenderService(IOptions<EmailConfigurationSettings> emailConfigurationOptions, ILogger<EmailSenderService> logger)
        {
            _emailConfig = emailConfigurationOptions.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            // Validate the email addresses
            try
            {
                var fromAddress = new MailAddress(_emailConfig.Username.Trim());
                var toAddress = new MailAddress(toEmail.Trim());
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid email format", nameof(toEmail), ex);
            }

            subject = subject.Replace(",", "");
            message = message.Replace(",", "");

            using var smtpClient = new SmtpClient(_emailConfig.SmtpServer, 587)
            {
                Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password),
                EnableSsl = true, // Enable SSL (Gmail requires this)
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.Username.Trim()),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("✅ Email sent successfully to {EmailAddress}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Failed to send email to {EmailAddress}", toEmail);
                throw new CustomException(500, $"Email sending failed: {ex.Message}");
            }
        }
    }
}
