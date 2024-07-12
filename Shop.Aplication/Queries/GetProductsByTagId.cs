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
    public class GetProductsByTagId:IRequest<PagingResponseModel<ProductPreviewResponseModel>?>
    {
        public Guid Id { get; set; }
        public int page {  get; set; }
        public int take {  get; set; }

        public GetProductsByTagId(Guid id, int page, int take)
        {
            Id = id;
            this.page = page;
            this.take = take;
        }
    }

    public class HandGetProductByTagId : IRequestHandler<GetProductsByTagId, PagingResponseModel<ProductPreviewResponseModel>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandGetProductByTagId(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResponseModel<ProductPreviewResponseModel>?> Handle(GetProductsByTagId request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.tagRepository.GetProductByTagId(request.Id, request.page, request.take);

                var count = await _unitOfWork.tagRepository.CountProductByTagId(request.Id);

                if(products is null || !products.Any()) return null;

                return new PagingResponseModel<ProductPreviewResponseModel>
                {
                    CurrentPage = request.page,
                    Data = products,
                    TotalPage = count / request.take,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
