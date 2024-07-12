using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.TagCommand
{
    public class RemoveTagCommand:IRequest<bool>
    {
        public Guid TagId { get; set; }

    }
    public class HandRemoveTagCommand : IRequestHandler<RemoveTagCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandRemoveTagCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.tagRepository.FindByIdAsync(request.TagId);

                if (result == null)  return false;

                _unitOfWork.tagRepository.Remove(result);

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
