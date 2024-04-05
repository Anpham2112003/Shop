using FluentValidation;
using Shop.Aplication.Commands.ProductCommand;

namespace Shop.Aplication.Validation;

public class UpdateProductValidation:AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Name)
            .NotNull()
            .Length(10, 250);

        RuleFor(x => x.Description)
            .NotNull()
            .Length(10, 250);

        RuleFor(x => x.Quantity)
            .NotNull()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThanOrEqualTo(1000);

        RuleFor(x => x.BrandId)
            .NotNull()
            .NotEmpty();
        
        

    }
}