using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using MyTaskApp.Models.MailModels;

namespace MyTaskApp.Infrastracture.Services
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly SMTPSettings _smtpSettings;
        public EmailSender(IOptions<SMTPSettings> options) {

            _smtpSettings = options.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string? htmlMessage)
        {
            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                client.EnableSsl = _smtpSettings.EnableSSL;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                MailMessage mailMessage = new MailMessage()
                {
                    
                    From = new MailAddress(_smtpSettings.From),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,

                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
            
        }
    }
}
