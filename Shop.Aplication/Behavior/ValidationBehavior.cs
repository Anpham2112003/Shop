using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace Shop.Aplication.Validation;

public class ValidationBehavior< TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse> where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        
        var context = new ValidationContext<TRequest>(request);
        var validation = _validators.Select(x => x.Validate(context));
        var errors = validation
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Select(x=> new ValidationFailure(x.PropertyName,x.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }
        
        
        return await next();

    }
}