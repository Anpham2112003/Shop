namespace Shop.Infratructure.Services;

public interface IMailerSeverive
{
    public Task SendMail(string? to, string subject, string body);
}