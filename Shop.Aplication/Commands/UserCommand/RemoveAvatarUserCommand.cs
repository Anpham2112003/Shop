using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.UserCommand
{
    public class RemoveAvatarUserCommand:IRequest<bool>
    {

    }

    public class HandRemoveAvatarUserCommand : IRequestHandler<RemoveAvatarUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public HandRemoveAvatarUserCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> Handle(RemoveAvatarUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

                var User = await _unitOfWork.userRepository.FindByIdAsync(UserId);

                User!.AvatarUrl = null;

                _unitOfWork.userRepository.Update(User);

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
