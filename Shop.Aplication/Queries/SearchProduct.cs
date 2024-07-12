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
    public class SearchProduct:IRequest<ScrollPageResponseModel<ProductPreviewResponseModel>>
    {
        public string? Name {  get; set; }
        public int Skip {  get; set; }
        public int Take { get; set; }

        public SearchProduct(string name, int skip, int take)
        {
            Name = name;
            Skip = skip;
            Take = take;
        }
    }

    public class HandSearchProduct : IRequestHandler<SearchProduct, ScrollPageResponseModel<ProductPreviewResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandSearchProduct(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ScrollPageResponseModel<ProductPreviewResponseModel>> Handle(SearchProduct request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.productRepository.SearchProduct(request.Name!, request.Skip, request.Take);

            return new ScrollPageResponseModel<ProductPreviewResponseModel>
            {
                Data = result,
                skip = request.Skip,
                take = request.Take
            };
        }
    }
}
