using FluentValidation;
using Shop.Aplication.Commands.CartCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveCartValidation : AbstractValidator<DeleteCartCommand>
    {
        public RemoveCartValidation()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
        }
    }
}
