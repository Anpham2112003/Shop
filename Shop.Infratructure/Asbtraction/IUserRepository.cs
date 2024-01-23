using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Asbtraction
{
    public interface IUserRepository:IRepository<User>
    {
    }
}
