using MediatR;
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

    public HandCommentCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
            var comment = await _unitOfWork.commentRepository.FindByIdAsync(request.Id);

            if (comment is null ) return false;
        
            _unitOfWork.commentRepository.Remove(comment); 
        
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}