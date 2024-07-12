using FluentValidation;
using Shop.Aplication.Commands.CategoryCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UnCategoryValidation : AbstractValidator<UnCategoryProductCommand>
    {
        public UnCategoryValidation()
        {
            RuleFor(x=>x.CategoryId).NotNull().NotEmpty();
            RuleFor(x=>x.ProductId).NotNull().NotEmpty();
        }
    }
}
