using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Shop.Infratructure.Services;

public class MailerService:IMailerSeverive
{
    private readonly IConfiguration _configuration;

    public MailerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMail(string to, string subject, string body)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(_configuration["Mailer:From"]);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;
        message.BodyEncoding=Encoding.UTF8;

        using var client= new SmtpClient(_configuration["Mailer:Host"],int.Parse(_configuration["Mailer:Port"]));
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential (_configuration["Mailer:Account"].ToString(),_configuration["Mailer:Password"].ToString() );
        client.EnableSsl = true;

        await client.SendMailAsync(message);
    }
}