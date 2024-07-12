using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shop.Domain.Ultils;
using Shop.Infratructure.Services.Aws3Service;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.UserCommand
{
    public class UpdateAvatarUserCommand:IRequest<string>
    {
        public IFormFile? file {  get; set; }
    }

    public class HandUpdateAvatarUserCommand : IRequestHandler<UpdateAvatarUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IAwsSevice _awsSevice;
        public HandUpdateAvatarUserCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IConfiguration configuration, IAwsSevice awsSevice)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _awsSevice = awsSevice;
        }

        public async Task<string> Handle(UpdateAvatarUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

                var User = await _unitOfWork.userRepository.FindByIdAsync(UserId);

                var avatarUrl = new StringBuilder()
                    .Append(Guid.NewGuid().ToString())
                    .Append(Path.GetExtension(request.file!.FileName)).ToString();

                User!.AvatarUrl = _configuration["Aws:Url"] + avatarUrl;

                await _awsSevice.Upload(request.file, _configuration["Aws:Bucket"]!, avatarUrl,cancellationToken);

                _unitOfWork.userRepository.Update(User!);

                await _unitOfWork.SaveChangesAsync();

                return User!.AvatarUrl;
                
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
