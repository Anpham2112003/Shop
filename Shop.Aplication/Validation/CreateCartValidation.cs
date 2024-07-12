using FluentValidation;
using Shop.Aplication.Commands.CartCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateCartValidation : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x=>x.ProductId)
                .NotEmpty()
                .NotNull();

         

            RuleFor(x=>x.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

           
          
        }
    }
}
