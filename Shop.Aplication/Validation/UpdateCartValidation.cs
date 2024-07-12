using FluentValidation;
using Shop.Aplication.Commands.CartCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateCartValidation : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

           

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1);

           
        }
    }
}
