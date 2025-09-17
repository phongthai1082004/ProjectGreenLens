using Microsoft.Extensions.Options;
using MimeKit;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            // Kết nối đến SMTP server
            await client.ConnectAsync(
                _settings.SmtpHost,
                _settings.SmtpPort,
                _settings.EnableSsl ? MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.None
            );

            // Xác thực
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
