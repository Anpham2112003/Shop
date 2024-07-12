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
    public class GetProductsInCategory:IRequest<PagingResponseModel<ProductPreviewResponseModel>?>
    {
        public Guid id { get; set; }
        public int page {  get; set; }
        public int take { get; set; }

        public GetProductsInCategory(Guid id, int page, int take)
        {
            this.id = id;
            this.page = page;
            this.take = take;
        }
    }

    public class HandGetProductInCategory : IRequestHandler<GetProductsInCategory, PagingResponseModel<ProductPreviewResponseModel>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandGetProductInCategory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResponseModel<ProductPreviewResponseModel>?> Handle(GetProductsInCategory request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.categoryRepository.GetProductInCategory(request.id, request.page, request.take);

                var totalProduct = await _unitOfWork.categoryRepository.CountProductInCategory(request.id);

                if (products == null || !products.Any()) return null;

                return new PagingResponseModel<ProductPreviewResponseModel>
                {
                    CurrentPage= request.page,
                    TotalPage= totalProduct /request.take,
                    Data=products
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
