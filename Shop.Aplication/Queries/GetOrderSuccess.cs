using MediatR;
using Shop.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetOrderSuccess:IRequest<PagingResponseModel<OrderResponseModel>>
    {
        public int page {  get; set; }
        public int take {  get; set; }
    }

    public class HandGetOrderSuccess : IRequestHandler<GetOrderSuccess, PagingResponseModel<OrderResponseModel>>
    {
        public Task<PagingResponseModel<OrderResponseModel>> Handle(GetOrderSuccess request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
