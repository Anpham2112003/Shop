using AutoMapper;
using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetUserByIdQuery:IRequest<UsersResponseModel>
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid _Id)
        {
            Id= _Id;
        }

    }
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UsersResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UsersResponseModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var Query = await _unitOfWork.userRepository.GetByIdAsync(request.Id);
            var Result= _mapper.Map<UsersResponseModel>(Query);
            return Result;
        }
    }
}
