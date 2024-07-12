using FluentValidation;
using Shop.Aplication.Commands.TagCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveTagValidation : AbstractValidator<RemoveTagCommand>
    {
        public RemoveTagValidation()
        {
            RuleFor(x=>x.TagId).NotNull().NotEmpty();
        }
    }
}
