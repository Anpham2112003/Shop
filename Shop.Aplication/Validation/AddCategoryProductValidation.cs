using FluentValidation;
using Shop.Aplication.Commands.CategoryCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class AddCategoryProductValidation : AbstractValidator<AddProducToCategoryCommand>
    {
        public AddCategoryProductValidation()
        {
            RuleFor(x=>x.ProductId).NotNull().NotEmpty();
            RuleFor(x=>x.CategoryId).NotNull().NotEmpty();
        }
    }
}
