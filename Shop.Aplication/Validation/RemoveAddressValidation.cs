using FluentValidation;
using Shop.Aplication.Commands.AddressCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveAddressValidation:AbstractValidator<DeleteAddressCommand>
    {
        public RemoveAddressValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotEmpty();
        }
    }
}
