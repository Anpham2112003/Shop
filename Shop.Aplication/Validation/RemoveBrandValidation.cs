using FluentValidation;
using Shop.Aplication.Commands.BrandCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveBrandValidation : AbstractValidator<DeleteBrandCommand>
    {
        public RemoveBrandValidation()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
        }
    }
}
