using FluentValidation;
using Shop.Aplication.Commands.AddressCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateAddressValidation : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x=>x.StreetAddress)
                .NotEmpty()
                .NotNull()
                .Length(5,250);

            RuleFor(x=>x.District)
                .NotNull()
                .NotEmpty()
                .Length(5,50);

            RuleFor(x => x.Commune)
                .NotEmpty()
                .NotEmpty()
                .Length(5, 50);

            RuleFor(x=>x.City)
                .NotEmpty()
                .NotNull()
                .Length (5, 50);

          
        }
    }
}
