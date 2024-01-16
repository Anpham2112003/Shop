using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Asbtraction
{
    public interface IUserRepository<Entity> where Entity : BaseEntity
    {
        public Task<Entity> GetUserById(Guid Id);
        public Task<List<Entity>> GetAll();
        public Task<int> DeleteUserById(Guid Id);
        public Task<int> UpdateUserById(Guid Id);
    }
}
