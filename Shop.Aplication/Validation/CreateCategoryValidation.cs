using FluentValidation;
using Shop.Aplication.Commands.BrandCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateCategoryValidation : AbstractValidator<CreateBrandCommand>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .Length(5,50);
        }
    }
}
