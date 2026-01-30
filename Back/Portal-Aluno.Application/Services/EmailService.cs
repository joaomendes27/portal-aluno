using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Portal_Aluno.Domain.Interfaces;

namespace Portal_Aluno.Application.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService(IConfiguration configuration)
    {
        _smtpServer = configuration["EmailSettings:SmtpServer"] ?? "";
        _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] ?? "587");
        _smtpUser = configuration["EmailSettings:SmtpUser"] ?? "";
        _smtpPass = configuration["EmailSettings:SmtpPass"] ?? "";
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };
        var mail = new MailMessage(_smtpUser, to, subject, body) { IsBodyHtml = true };
        await client.SendMailAsync(mail);
    }
}
