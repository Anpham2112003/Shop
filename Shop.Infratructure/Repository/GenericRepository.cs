using Microsoft.EntityFrameworkCore;
using Shop.Domain.Abstraction;
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
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class, BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
        }

        public async Task AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddRangeAsync(entity);
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            _context.Set<Entity>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<Entity> entities)
        {
            await _context.Set<Entity>().AddRangeAsync(entities);
        }

        public IEnumerable<Entity> Find(Expression<Func<Entity, bool>> expression)
        {
            var Result=_context.Set<Entity>().Where(expression).ToList();
            return Result;
        }

        public IEnumerable<Entity> GetAll()
        {
            var Results=_context.Set<Entity>().ToList();
            return Results;
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            var Result= await _context.Set<Entity>().ToListAsync();
            return Result;
        }

        public  Entity GetById(Guid id)
        {
            var Result = _context.Set<Entity>().Find(id);
            return Result;
        }

        public async Task<Entity> GetByIdAsync(Guid id)
        {
            var Result = await _context.Set<Entity>().FindAsync(id);
            return Result;
        }

        public void Remove(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            _context.Set<Entity>().RemoveRange(entities);
        }
    }
}
