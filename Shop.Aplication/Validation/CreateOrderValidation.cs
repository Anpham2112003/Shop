using FluentValidation;
using Shop.Aplication.Commands.OrderCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateOrderValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

        }
    }
}
