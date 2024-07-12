using FluentValidation;
using Shop.Aplication.Commands;

namespace Shop.Aplication.Validation;

public class UpdateUserValidation:AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidation()
    {
        
        RuleFor(x => x.FistName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .Length(2, 150);
        
    
        


    }
}