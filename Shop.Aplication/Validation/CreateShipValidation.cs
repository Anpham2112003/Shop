using FluentValidation;
using Shop.Aplication.Commands.ShipCommnd;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateShipValidation : AbstractValidator<CreateShipCommand>
    {
        public CreateShipValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ShipState)
                .NotEmpty()
                .NotNull()
                .IsInEnum();

            RuleFor(x => x.AddressId)
                .NotEmpty()
                .NotNull();

            RuleFor(x=>x.OrderId)
                .NotNull()
                .NotEmpty();

        }
    }
}
