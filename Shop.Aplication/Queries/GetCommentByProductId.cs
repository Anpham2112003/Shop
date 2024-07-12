using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetCommentByProductId:IRequest<ScrollPageResponseModel<CommentResponseModel>>
{
    public GetCommentByProductId(Guid id, int skip, int take)
    {
        Id = id;
        Skip = skip;
        Take = take;
    }

    public Guid Id { get; set; }
    
    public int Skip { get; set; }
    
    public int Take { get; set; }
}

public class HandGetCommentByProductId : IRequestHandler<GetCommentByProductId,ScrollPageResponseModel<CommentResponseModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetCommentByProductId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ScrollPageResponseModel<CommentResponseModel>> Handle(GetCommentByProductId request, CancellationToken cancellationToken)
    {
        try
        {
            
            var result = await _unitOfWork.commentRepository.GetCommentByProductId(request.Id, request.Skip, request.Take);



            return new ScrollPageResponseModel<CommentResponseModel>
            {
                Data = result,
                skip = request.Skip,
                take = request.Take
            };

            
        }
        catch (Exception )
        {
            throw;
        }
        
        
        
    }
}