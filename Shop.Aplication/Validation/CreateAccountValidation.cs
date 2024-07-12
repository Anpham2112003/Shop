using FluentValidation;
using Shop.Aplication.Commands.AuthCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateAccountValidation : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidation()
        {
            RuleFor(x=>x.Email).EmailAddress().NotEmpty();

            RuleFor(x=>x.FistName).Length(1,255).NotEmpty();
            RuleFor(x=>x.LastName).Length(1,255).NotEmpty();
            RuleFor(x=>x.FullName).Length(1,255).NotEmpty();
        }
    }
}
