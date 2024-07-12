using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CommentCommand;

public class DeleteCommentCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteCommentCommand(Guid id)
    {
        Id = id;
    }
}
public class HandCommentCommand:IRequestHandler<DeleteCommentCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public HandCommentCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            var comment = await _unitOfWork.commentRepository.FindByIdAsync(request.Id);

            if (comment is null || comment.UserId != UserId ) return false;
        
            _unitOfWork.commentRepository.Remove(comment); 
        
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception )
        {
            throw ;
        }

    }
}