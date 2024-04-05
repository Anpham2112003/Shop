using AutoMapper;
using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.UserQueries;

public class GetInfoUserById : IRequest<UserResponseModel?>
{
    public Guid Id { get; set; }

    public GetInfoUserById(Guid id)
    {
        Id = id;
    }
}
public class HandGetUserById:IRequestHandler<GetInfoUserById,UserResponseModel?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandGetUserById(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponseModel?> Handle(GetInfoUserById request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.GetInfoUserById(request.Id);
            
            if (user is null) return null;

            var userResponse = _mapper.Map<UserResponseModel>(user);

            return userResponse;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       
        
    }
}