using FluentValidation;
using Shop.Aplication.Commands.TagCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateTagValidation : AbstractValidator<CreateTagCommand>
    {
        public CreateTagValidation()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty();
        }
    }
}
