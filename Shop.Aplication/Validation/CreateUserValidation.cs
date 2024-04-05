using FluentValidation;
using Shop.Aplication.Commands;

namespace Shop.Aplication.Validation;

public class CreateUserValidation:AbstractValidator<CreateUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.FistName)
            .NotEmpty()
            .Length(2, 25);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 25);
        
        RuleFor(x => x.FullName)
            .NotEmpty()
            .Length(4, 50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 250);
        
    }
}