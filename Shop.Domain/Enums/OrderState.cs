using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Enums
{
    public enum OrderState
    {
        PaymentOnDelivery=0,
        WaitPayment =1,
        Paid=2,
        Success=3,
        Faild=4
    }
}
