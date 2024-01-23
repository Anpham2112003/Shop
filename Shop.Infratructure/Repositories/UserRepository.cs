using Microsoft.EntityFrameworkCore;
using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.Asbtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> DeleteById(Guid Id)
        {
            
            throw new NotImplementedException();
        }

        public Task<int> DeleteRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>>? GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid Id)
        {
          return await _context.Users!.FindAsync(Id);
        }

        public async Task<int> InsertRangeAsync(IEnumerable<User> entities)
        {
            await _context.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

