using MediatR;
using Shop.Infratructure.Services;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Notify;

public class HandPaymentNotification:INotificationHandler<PaymentNotification>
{
    private readonly IMailerSeverive _mailerSeverive;
    private readonly IUnitOfWork _unitOfWork;
    public HandPaymentNotification(IMailerSeverive mailerSeverive, IUnitOfWork unitOfWork)
    {
        _mailerSeverive = mailerSeverive;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(PaymentNotification notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.userRepository.GetInfoUserById(notification.order.UserId);
        await _mailerSeverive.SendMail(user.Email,"Thong bao thanh toan don hang ", 
            $@"Don hang {notification.order.Id} 
            cua ban duoc thanh toan thanh cong voi so tien la : {notification.order.TotalPrice} !");
       
        await Task.CompletedTask;
    }
}