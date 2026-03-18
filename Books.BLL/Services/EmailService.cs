using Books.BLL.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Books.BLL.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<ServiceResponse> SendAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using var client = new SmtpClient(_settings.Host, _settings.Port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_settings.UserName, _settings.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail, _settings.FromName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                message.To.Add(toEmail);

                await client.SendMailAsync(message);

                return new ServiceResponse { Message = "Лист успішно відправлено" };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Помилка відправки листа: {ex.Message}"
                };
            }
        }
    }
}