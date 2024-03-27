using MediatR;
using Shop.Infratructure.Services;

namespace Shop.Aplication.Notify;

public class HandPaymentNotify:INotificationHandler<PaymentNotify>
{
    private readonly IMailerSeverive _mailerSeverive;

    public HandPaymentNotify(IMailerSeverive mailerSeverive)
    {
        _mailerSeverive = mailerSeverive;
    }

    public async Task Handle(PaymentNotify notification, CancellationToken cancellationToken)
    {
        await _mailerSeverive.SendMail(notification.To, notification.Subject, notification.Message);
       
        await Task.CompletedTask;
    }
}