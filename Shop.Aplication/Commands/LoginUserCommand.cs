using MediatR;
using Microsoft.Extensions.Options;
using Shop.Aplication.ResultOrError;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands
{
    public class LoginUserCommand:IRequest<IResult<LoginResponseModel>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public LoginUserCommand(string? email, string? password)
        {
            Email = email;
            Password = password;
        }
    }

    public class HandLoginUserCommand : IRequestHandler<LoginUserCommand, IResult<LoginResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptionsMonitor<JwtOp>
        public HandLoginUserCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<LoginResponseModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user=  _unitOfWork.userRepository.Find(x=> x.Email == request.Email).FirstOrDefault();
            if (user == null)
            {
                return new NotFound<LoginResponseModel>("User not exist");
            }

            if (user.Password != request.Password)
            {
                return new UnAuthorization<LoginResponseModel>("Unauthori");
            }


        }



        public Task<string> GennerateToken(User user , string key)
        {
            var Claim = new Claim[] {
                new Claim(ClaimTypes.Actor,)
            }
        }
    }
}
