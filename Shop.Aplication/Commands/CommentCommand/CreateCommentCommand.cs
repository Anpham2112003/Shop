using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CommentCommand;

public class CreateCommentCommand:IRequest<bool>
{
    public Guid Id=Guid.NewGuid();
    public Guid ProductId { get; set; }
    public string? Content {  get; set; }
    public int Rate {  get; set; }
   
}

public class HandCreateCommentCommand : IRequestHandler<CreateCommentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    public HandCreateCommentCommand(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());
        
            var product = await _unitOfWork.productRepository.FindByIdAsync(request.ProductId);

            if ( product is null) return false;

            var comment = _mapper.Map<Comment>(request);

            comment.UserId = UserId;
            
            await _unitOfWork.commentRepository.AddAsync(comment);
            
            await _unitOfWork.SaveChangesAsync();

            return true;

        }
        catch (Exception )
        {
            throw ;
        }
        
        
    }
}