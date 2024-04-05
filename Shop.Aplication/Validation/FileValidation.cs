using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shop.Aplication.Validation;

public class FileValidation:AbstractValidator<IFormFile>
{
    public FileValidation()
    {
        RuleFor(x => x.Length)
            .NotNull()
            .GreaterThan(100)
            .WithMessage("File size small");

        RuleFor(x => x.FileName)
            .NotNull()
            .Must(x => x.EndsWith(".png") || x.EndsWith(".jpeg"))
            .WithMessage("File extension not valid!");
    }
}