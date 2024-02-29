using MediatR;
using Shop.Aplication.ResultOrError;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands
{
    public class DeleteUserCommand:IRequest<IResult<DeleteUserCommand>>
    {
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

   
    }

    public class HandDeleteUserCommand : IRequestHandler<DeleteUserCommand, IResult<DeleteUserCommand>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandDeleteUserCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<DeleteUserCommand>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            

            try
            {
                var user = await _unitOfWork.userRepository.GetByIdAsync(request.Id);

                if (user == null) return new NotFound<DeleteUserCommand>("Cant not delete user because not found UserId= " + request.Id);
                

                _unitOfWork.userRepository.Remove(user);
                await _unitOfWork.SaveChangesAsync();
                return new Ok<DeleteUserCommand>("success",request);

            }
            catch (Exception ex)
            {
                return new ServerError<DeleteUserCommand>("Server error! " + ex);
                
            }
        }
    }
}
