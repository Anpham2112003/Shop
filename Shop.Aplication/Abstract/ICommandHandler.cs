using Shop.Aplication.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Abstract
{
    public interface ICommandHandler<Command> where Command : ICommand
    {
        void HandleCommand(Command command);
    }
}
