using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetHistoryPayment:IRequest<PagingResponseModel<Payment>?>
    {
        public int Page {  get; set; }
        public int Take {  get; set; }

        public GetHistoryPayment(int page, int take)
        {
            Page = page;
            Take = take;
        }
    }

    public class HandGetHistoryPayment : IRequestHandler<GetHistoryPayment,PagingResponseModel<Payment>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public HandGetHistoryPayment(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<PagingResponseModel<Payment>?> Handle(GetHistoryPayment request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

                var total = await _unitOfWork.paymentRepository.CountPaymentByUserId(userId);

                var result = await _unitOfWork.paymentRepository.GetPaymentByUserId(userId);

                if(!result.Any()) return null;

                return new PagingResponseModel<Payment>
                {
                    CurrentPage = request.Page,
                    Data = result,
                    TotalPage = total / request.Page,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
