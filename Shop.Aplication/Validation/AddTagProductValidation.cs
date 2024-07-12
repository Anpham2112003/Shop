using FluentValidation;
using Shop.Aplication.Commands.TagCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class AddTagProductValidation : AbstractValidator<AddTagToProductCommand>
    {
        public AddTagProductValidation()
        {
            RuleFor(x=>x.ProductId).NotNull().NotEmpty();

            RuleFor(x=>x.ProductId).NotNull().NotEmpty();  
        }
    }
}
