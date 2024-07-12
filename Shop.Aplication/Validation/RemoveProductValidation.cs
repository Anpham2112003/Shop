using FluentValidation;
using Shop.Aplication.Commands.ProductCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveProductValidation : AbstractValidator<DeleteProductCommand>
    {
        public RemoveProductValidation()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
        }
    }
}
