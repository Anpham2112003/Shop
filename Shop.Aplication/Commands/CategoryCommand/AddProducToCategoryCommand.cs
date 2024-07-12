using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.CategoryCommand
{
    public class AddProducToCategoryCommand:IRequest<bool>
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }

        public AddProducToCategoryCommand(Guid categoryId, Guid productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }
    }

    public class HandAddProductToCategory : IRequestHandler<AddProducToCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandAddProductToCategory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddProducToCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.categoryRepository.AddCategoryToProduct(request.CategoryId, request.ProductId);

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
