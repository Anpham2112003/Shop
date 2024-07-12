using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.TagCommand
{
    public class UnTagProductCommand:IRequest<bool>
    {
        public Guid TagId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class HandUnTagProduct : IRequestHandler<UnTagProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandUnTagProduct(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UnTagProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var find = await _unitOfWork.tagRepository.FindProductTag(request.TagId, request.ProductId);

                if (find == null) return false;

                 _unitOfWork.tagRepository.RemoveTagProduct(find);

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
