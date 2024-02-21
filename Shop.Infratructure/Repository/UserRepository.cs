using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
           return await _context.Set<User>().CountAsync();   
        }

        public async Task<IEnumerable<User>> GetAllAsync(int page, int take)
        {
            var Result= await _context.Set<User>()
                .Skip((page - 1) * take)
                .Take(take)
                .AsNoTracking()
                .Select(x => new User {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                    IsDeleted=x.IsDeleted,
                    Role = x.Role,
                })
                .ToListAsync();
            return Result;
        }
    }
}
