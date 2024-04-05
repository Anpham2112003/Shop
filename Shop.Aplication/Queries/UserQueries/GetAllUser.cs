using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.UserQueries;

public class GetAllUser:IRequest<IEnumerable<UsersResponseModel>?>
{
    public int Page { get; set; }
    public int Take { get; set; }

    public GetAllUser(int page, int take)
    {
        Page = page;
        Take = take;
    }
}

public class HandGetAllUser:IRequestHandler<GetAllUser,IEnumerable<UsersResponseModel>?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandGetAllUser(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsersResponseModel>?> Handle(GetAllUser request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _unitOfWork.userRepository.GetAllAsyncNoTracking(request.Page, request.Take);

            if (users != null )
            {
                var data = _mapper.Map<IEnumerable<User>, IEnumerable<UsersResponseModel>>(users);
            
                return data;
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       
    }
}
