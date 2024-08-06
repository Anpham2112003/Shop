using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.Services;
using Shop.Infratructure.UnitOfWork;
using System.Security.Claims;

namespace Shop.Aplication.Commands.AuthCommand
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public Guid Id = Guid.NewGuid();

        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public Guid RoleId = Guid.Parse("c1bb2db4-a327-431f-9d7b-5122d6e17c28");

        public  bool IsActive = true;
        public DateTime CreatedAt = DateTime.UtcNow;

    }
    public class HandCreateUserCommand : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailerSeverive _mailerSeverive;
        private readonly IConfiguration _configuration;
        public HandCreateUserCommand(IUnitOfWork unitOfWork, IMapper mapper, IMailerSeverive mailerSeverive, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailerSeverive = mailerSeverive;
            _configuration = configuration;
        }

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
          

            try
            {
                var user = _mapper.Map<User>(request);

                
                var findUser = await _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);

                if (findUser is not null) return false;

                await _unitOfWork.userRepository.AddAsync(user);

                await _unitOfWork.SaveChangesAsync();

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, request.Email!),
                    new Claim(ClaimTypes.PrimarySid,user.Id.ToString())
                };

                var codeActive = JwtUltil.GenerateToken(_configuration["EmailConfirm:VerifyUser"]!, claims, DateTime.UtcNow.AddYears(1));

                //await _mailerSeverive.SendMail(request.Email!,"VetifyAccount", EmailTemplate.VerifyEmail(codeActive));

                return true;
            }
            catch (Exception )
            {
                throw;
            }


        }
    }
}
