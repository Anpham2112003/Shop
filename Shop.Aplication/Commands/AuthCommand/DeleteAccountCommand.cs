using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.AuthCommand
{
    public class DeleteAccountCommand : IRequest<bool>
    {
       


    }

    public class HandDeleteUserCommand : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        public HandDeleteUserCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

                var user = await _unitOfWork.userRepository.FindByIdAsync(UserId);

                if (user == null) return false;

                user.IsDeleted = true;

                _unitOfWork.userRepository.Update(user);

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
