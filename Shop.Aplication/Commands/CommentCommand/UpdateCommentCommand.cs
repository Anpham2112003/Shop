using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CommentCommand;

public class UpdateCommentCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public int Rate { get; set; }
}

public class HandUpdateCommand:IRequestHandler<UpdateCommentCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public HandUpdateCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await _unitOfWork.commentRepository.FindByIdAsync(request.Id);

            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim()); 

            if (comment is null || comment.UserId.Equals(UserId) == false) return false;

            comment.Content = request.Content;
            
            comment.Rate = request.Rate;
            
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception )
        {
            throw ;
        }

    }
}