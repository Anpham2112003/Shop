using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CommentCommand;

public class UpdateCommentCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public int Rate { get; set; }
}

public class HandUpdateCommand:IRequestHandler<UpdateCommentCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandUpdateCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await _unitOfWork.commentRepository.FindByIdAsync(request.Id);

            if (comment is null || comment.UserId.Equals(request.UserId) == false) return false;

            comment.Content = request.Content;
            
            comment.Rate = request.Rate;
            
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}