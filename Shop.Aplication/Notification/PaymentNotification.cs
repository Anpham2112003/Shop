using MediatR;
using Shop.Infratructure.Services;
using StackExchange.Redis;
using Order = Shop.Domain.Entities.Order;

namespace Shop.Aplication.Notify;

public class PaymentNotification:INotification
{
    public PaymentNotification(Order order)
    {
        this.order = order;
    }


    public Order order { get; set; }



}




