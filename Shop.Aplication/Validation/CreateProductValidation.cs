using FluentValidation;
using Shop.Aplication.Commands.ProductCommand;
using Shop.Domain.Entities;

namespace Shop.Aplication.Validation;

public class CreateProductValidation:AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .Length(10, 250);

        RuleFor(x => x.Description)
            .NotNull();

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(1000)
            .LessThan(100000000);

        RuleFor(x => x.Quantity)
            .GreaterThan(1)
            .LessThan(10000)
            .NotNull();

        RuleFor(x => x.BrandId)
            .NotNull()
            .NotEmpty();

       

        RuleFor(x => x.Image)
            .NotNull()
            .NotEmpty()
            .SetValidator(new FileValidation());

    }
}