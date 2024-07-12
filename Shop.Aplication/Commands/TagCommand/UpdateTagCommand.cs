using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.TagCommand
{
    public class UpdateTagCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class HandUpdateTagCommand : IRequestHandler<UpdateTagCommand, bool>
    {
        private readonly IUnitOfWork  _unitOfWork;

        public HandUpdateTagCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _unitOfWork.tagRepository.FindByIdAsync(request.Id);

                if (tag == null) return false;

                tag.TagName = request.Name;

                 _unitOfWork.tagRepository.Update(tag);

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
