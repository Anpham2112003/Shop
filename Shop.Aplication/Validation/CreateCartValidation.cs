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

            RuleFor(x=>x.ProductName)
                .NotEmpty()
                .NotNull()
                .Length(5,250);

            RuleFor(x => x.TotalPrice)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1000);

            RuleFor(x=>x.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .NotNull();
          
        }
    }
}
