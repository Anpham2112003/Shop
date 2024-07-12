using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.CategoryCommand
{
    public class UnCategoryProductCommand:IRequest<bool>
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class HandUnCategoryProduct : IRequestHandler<UnCategoryProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandUnCategoryProduct(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UnCategoryProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var find = await _unitOfWork.categoryRepository.FindProductCategory(request.CategoryId,request.ProductId);

                if (find is null) return false;

                _unitOfWork.categoryRepository.RemoveProductCatwgory(find);

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
