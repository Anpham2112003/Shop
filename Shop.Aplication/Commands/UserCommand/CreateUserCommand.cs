using AutoMapper;
using MediatR;
using Shop.Aplication.Notify;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands
{
    public class CreateUserCommand:IRequest<bool>
    {
        public Guid Id => Guid.NewGuid();

        public string? FistName { get; set; }
        public string? LastName {  get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public Guid RoleId=Guid.Parse("c1bb2db4-a327-431f-9d7b-5122d6e17c28");

        public readonly bool IsActive = false;
        public DateTime CreatedAt = DateTime.UtcNow;

    }
    public class HandCreateUserCommand : IRequestHandler<CreateUserCommand, bool>
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

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var findUser =await _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);
            if (findUser is not null) return false;
            var user = _mapper.Map<User>(request);
            try
            {
                await _unitOfWork.userRepository.AddAsync(user);
                
                await  _unitOfWork.SaveChangesAsync();
                
                await _publisher.Publish(new CreateUserNotification(request), cancellationToken);
                
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }
    }
}
