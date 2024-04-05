using FluentValidation;
using Shop.Aplication.Commands.CommentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class UpdateCommentValidation : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .Length(1, 255);

            RuleFor(x => x.Rate)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(5)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
