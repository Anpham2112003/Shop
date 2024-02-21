using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Aplication.ResultOrError;

namespace Shop.Domain.RequestModel
{
    public class CreateUserCommand:IRequest<IResult<CreateUserCommand>>
    {
        public Guid Id
        {
            get
            {
                return Guid.NewGuid();
            }
            
        }

        public string? FistName { get; set; }
        public string? LastName {  get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Role
        {
            get
            {
                return "User";
            }
        }
        public DateTime CreatedAt = DateTime.UtcNow;

    }
    public class HandCreateUserCommand : IRequestHandler<CreateUserCommand, IResult<CreateUserCommand>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HandCreateUserCommand(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<CreateUserCommand>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var findUser = _unitOfWork.userRepository.Find(x => x.Email == request.Email).FirstOrDefault();
            if (findUser is not null)
            {
                return new  BadRequest<CreateUserCommand>("Account already exists!");
            }
            var user = _mapper.Map<User>(request);
            try
            {
                await _unitOfWork.userRepository.AddAsync(user);
                await  _unitOfWork.SaveChangesAsync();
                return new Created<CreateUserCommand>("Created success", request);
            }
            catch (Exception e)
            {
                return new ServerError<CreateUserCommand>("Cant create new user because sever error!");
            }

            
        }
    }
}
