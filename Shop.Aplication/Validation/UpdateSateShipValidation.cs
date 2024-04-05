using FluentValidation;
using Shop.Aplication.Commands.ShipCommnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateSateShipValidation : AbstractValidator<UpdateShipCommand>
    {
        public UpdateSateShipValidation()
        {
            RuleFor(x=>x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ShipState)
                .NotEmpty()
                .NotNull()
                .IsInEnum();

        
        }
    }
}
