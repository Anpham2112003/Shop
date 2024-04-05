using FluentValidation;
using Shop.Aplication.Commands.BrandCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateCategoryValidation()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .NotNull()
                .Length(5,50);
        }
    }
}
