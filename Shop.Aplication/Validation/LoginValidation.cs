using FluentValidation;
using Shop.Aplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class LoginValidation : AbstractValidator<LoginAccountCommand>
    {
        public LoginValidation()
        {
            RuleFor(x=>x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotNull().Length(8, 255);
        }
    }
}
