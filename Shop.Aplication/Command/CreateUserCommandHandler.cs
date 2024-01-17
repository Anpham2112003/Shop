using Shop.Aplication.Abstract;
using Shop.Domain.Entities;
using Shop.Infratructure.Asbtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Command
{
    
    public class CreateUserCommandHandler:ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;
        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void HandleCommand(CreateUserCommand command)
        {

           var User= _userRepository.GetById(command.Id);
            if(User is not null)
            {
                throw new ArgumentException("User da ton tai");
            }


        }

        
    }
}
