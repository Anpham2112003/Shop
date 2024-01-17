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
    public class UserRepository : IRepository<User>
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

        public async Task<List<User>> GetAll()
        {
            var Users = await _context.User.ToListAsync();
            return Users;
        }

        public async Task<User> GetById(Guid Id)
        {
            var  User =  await _context.User.FindAsync(Id);
            return User;
        }

        public Task<int> UpdateById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

