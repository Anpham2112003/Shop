using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.TagCommand
{
    public class AddTagToProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }

    }

    public class HandAddTagToProduct : IRequestHandler<AddTagToProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandAddTagToProduct(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddTagToProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.tagRepository.AddTagToProduct(request.TagId, request.ProductId);

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
