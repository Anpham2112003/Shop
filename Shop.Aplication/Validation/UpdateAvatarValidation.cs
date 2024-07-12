using FluentValidation;
using Shop.Aplication.Commands.UserCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateAvatarValidation : AbstractValidator<UpdateAvatarUserCommand>
    {
        public UpdateAvatarValidation()
        {
            RuleFor(x => x.file).NotNull().NotEmpty().SetValidator(new FileValidation());
        }
    }
}
