using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.CommentQueries;

public class GetCommentByProductId:IRequest<PagingResponseModel<List<Comment>>>
{
    public GetCommentByProductId(Guid id, int page, int take)
    {
        Id = id;
        Page = page;
        Take = take;
    }

    public Guid Id { get; set; }
    
    public int Page { get; set; }
    
    public int Take { get; set; }
}

public class HandGetCommentByProductId : IRequestHandler<GetCommentByProductId,PagingResponseModel< List<Comment>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetCommentByProductId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<List<Comment>>> Handle(GetCommentByProductId request, CancellationToken cancellationToken)
    {
        try
        {
            
            var result = await _unitOfWork.commentRepository.GetCommentByProductId(request.Id, request.Page, request.Take);
            
            var total = await _unitOfWork.commentRepository.CountCommentByProductId(request.Id);

            return new PagingResponseModel<List<Comment>>()
            {
                Message = "success",
                Total = total,
                Data = result,
            };

            
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
        
        
    }
}