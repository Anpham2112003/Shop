using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetOrderByIdUserQuery:IRequest<IEnumerable<Order>>
    {
        public Guid Id { get; set; }
        public int page {  get; set; }
        public int take {  get; set; }
        public GetOrderByIdUserQuery(Guid _Id,int _page,int _take)
        {
            Id = _Id;
            page = _page;
            take = _take;
        }
    }

    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdUserQuery, IEnumerable<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrderByIdUserQuery request, CancellationToken cancellationToken)
        {
            var Result = await _unitOfWork.orderRepository.GetOrderNoPaymentByUserId(request.Id, request.page, request.take);
            return Result;
        }
    }
}
