using FluentValidation;
using Shop.Aplication.Commands.OrderCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveOrderValidation : AbstractValidator<RemoveOrderCommand>
    {
        public RemoveOrderValidation()
        {
            RuleFor(x => x.OrderId).NotNull().NotEmpty();
        }
    }
}
