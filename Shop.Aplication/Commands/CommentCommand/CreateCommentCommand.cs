using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CommentCommand;

public class CreateCommentCommand:IRequest<bool>
{
    public Guid Id=Guid.NewGuid();
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public Guid ProductId { get; set; }
    public string? Content {  get; set; }
    public int Rate {  get; set; }
   
}

public class HandCreateCommentCommand : IRequestHandler<CreateCommentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandCreateCommentCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.FindByIdAsync(request.UserId);
        
            var product = await _unitOfWork.productRepository.FindByIdAsync(request.Id);

            if (user is null || product is null) return false;

            var comment = _mapper.Map<Comment>(request);
            
            await _unitOfWork.commentRepository.AddAsync(comment);
            
            await _unitOfWork.SaveChangesAsync();

            return true;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
        
    }
}