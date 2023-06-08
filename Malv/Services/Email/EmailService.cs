using System.Net;
using System.Net.Mail;
using Malv.Data;
using Microsoft.Extensions.Options;

namespace Malv.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _smtpOptions;
    
    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }
    
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var client = new SmtpClient(_smtpOptions.Client, _smtpOptions.Port)
        {
            Credentials = new NetworkCredential(_smtpOptions.FromMail, _smtpOptions.FromPassword),
            EnableSsl = true
        };
        var msg = new MailMessage()
        {
            From = new MailAddress(_smtpOptions.FromMail, "No-reply"),
            Subject = subject,
            Body = message,
            IsBodyHtml = false
        };
        msg.To.Add(new MailAddress(toEmail));

        await client.SendMailAsync(msg);
    }
}