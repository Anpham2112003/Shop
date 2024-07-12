using FluentValidation;
using Shop.Aplication.Commands.CommentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Validation
{
    public class RemoveCommentValidation : AbstractValidator<DeleteCommentCommand>
    {
        public RemoveCommentValidation()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
        }
    }
}
