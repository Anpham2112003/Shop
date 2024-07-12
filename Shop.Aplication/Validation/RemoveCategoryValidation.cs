using FluentValidation;
using Shop.Aplication.Commands.CategoryCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveCategoryValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public RemoveCategoryValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().NotNull();
        }
    }
}
