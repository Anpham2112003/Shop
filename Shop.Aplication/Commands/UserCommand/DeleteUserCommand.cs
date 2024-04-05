using MediatR;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands
{
    public class DeleteUserCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

   
    }

    public class HandDeleteUserCommand : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandDeleteUserCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            

            try
            {
                var user = await _unitOfWork.userRepository.FindByIdAsync(request.Id);

                if (user == null) return false;
                

                _unitOfWork.userRepository.Remove(user);
                await _unitOfWork.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
