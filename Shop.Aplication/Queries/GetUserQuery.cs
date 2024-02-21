using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        public int page;
        public int take;
        public GetUsersQuery(int _page, int _take)
        {
            page = _page;
            take = _take;
        }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var Result = await _unitOfWork.userRepository.GetAllAsync(request.page, request.take);
            return Result;

        }
    }
}
