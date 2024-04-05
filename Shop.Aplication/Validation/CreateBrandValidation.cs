using FluentValidation;
using Shop.Aplication.Commands.BrandCommand;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateBrandValidation : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandValidation()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .Length(3,50);
        }
    }
}
