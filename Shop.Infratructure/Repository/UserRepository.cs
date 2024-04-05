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

        public async Task<User?> GetUserByIdAndRole(Guid id)
        {
            var result = await _context.Set<User>()
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
            
            return result;
        }

        public Task<User?> GetUserByEmailAndRole(string? email)
        {
            var result = _context.Set<User>()
                .Where(x => x.Email == email)
                .Include(x=>x.Role)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> Count()
        {
           return await _context.Set<User>().CountAsync();   
        }

        public async Task<User?> GetInfoUserById(Guid id)
        {
            var user = await _context.Set<User>()
                .Where(x => x.Id == id)
                .Select(x => new User()
                {
                    Id = x.Id,
                    FistName = x.FistName,
                    LastName = x.LastName,
                    FullName = x.FullName,
                    Email = x.Email
                }).FirstOrDefaultAsync();
            return user;
        }


        public async Task<List<User>?> GetAllAsyncNoTracking(int page, int take)
        {
            var result= await _context.Set<User>()
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
            return result;
        }
    }
}
