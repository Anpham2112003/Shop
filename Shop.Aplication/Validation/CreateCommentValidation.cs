using FluentValidation;
using Shop.Aplication.Commands.CommentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class CreateCommentValidation : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x=>x.ProductId)
                .NotNull()
                .NotEmpty();

            RuleFor(x=>x.Content)
                .NotEmpty()
                .NotNull()
                .Length(1,255);

            RuleFor(x => x.Rate)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(5)
                .GreaterThanOrEqualTo(1);

           

        }
    }
}
