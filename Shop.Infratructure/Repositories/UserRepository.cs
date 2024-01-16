using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using Shop.Infratructure.Asbtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Repositories
{
    public class UserRepository<TEntity> : IUserRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<int> DeleteUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        Task<List<TEntity>> IUserRepository<TEntity>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IUserRepository<TEntity>.GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

