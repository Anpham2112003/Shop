using AutoMapper;
using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetProfileUser : IRequest<UserResponseModel?>
{
    public Guid Id { get; set; }

    public GetProfileUser(Guid id)
    {
        Id = id;
    }
}
public class HandGetProfileUser : IRequestHandler<GetProfileUser, UserResponseModel?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandGetProfileUser(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponseModel?> Handle(GetProfileUser request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.GetInfoUserById(request.Id);

            if (user is null) return null;

            var userResponse = _mapper.Map<UserResponseModel>(user);

            return userResponse;
        }
        catch (Exception)
        {
            throw;
        }


    }
}