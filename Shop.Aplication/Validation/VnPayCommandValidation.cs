using FluentValidation;
using Shop.Aplication.Commands.PaymentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class VnPayCommandValidation : AbstractValidator<VnPayCommand>
    {
        public VnPayCommandValidation()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
        }
    }
}
