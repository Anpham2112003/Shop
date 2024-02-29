using AutoMapper;
using MediatR;
using Shop.Aplication.Notify;
using Shop.Aplication.ResultOrError;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands
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

        public Guid RoleId
        {
            get
            {
                return Guid.Parse("c52b4602-cd0a-4562-8d4e-13da5dda13cc");
            }
        }

        public bool IsActive = false;
        public DateTime CreatedAt = DateTime.UtcNow;

    }
    public class HandCreateUserCommand : IRequestHandler<CreateUserCommand, IResult<CreateUserCommand>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        
        public HandCreateUserCommand(IUnitOfWork unitOfWork, IMapper mapper, IPublisher publisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<IResult<CreateUserCommand>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var findUser =await _unitOfWork.userRepository.GetUserByEmail(request.Email);
            if (findUser is not null)
            {
                return new  BadRequest<CreateUserCommand>("Account already exists!");
            }
            var user = _mapper.Map<User>(request);
            try
            {
                await _unitOfWork.userRepository.AddAsync(user);
                await  _unitOfWork.SaveChangesAsync();
                await _publisher.Publish(new CreateUserNotify(request), cancellationToken);
                return new Created<CreateUserCommand>("Created success", request);
            }
            catch (Exception e)
            {
                return new ServerError<CreateUserCommand>("Cant create new user because sever error!");
            }

            
        }
    }
}
