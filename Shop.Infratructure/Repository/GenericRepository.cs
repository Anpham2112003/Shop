using Microsoft.EntityFrameworkCore;
using Shop.Domain.Abstraction;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shop.Infratructure.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class, BaseEntity
    {
        private readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Entity?> FindByIdAsync(Guid id)
        {
            var result = await _context.Set<Entity>().FirstOrDefaultAsync(x => x.Id == id);
            return result;
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

        public void Update(Entity entity)
        {
            _context.Set<Entity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<Entity> entity)
        {
            _context.Set<Entity>().UpdateRange(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Entity> entities)
        {
            await _context.Set<Entity>().AddRangeAsync(entities);
        }

        public async Task<bool> Check(Guid id)
        {
            var entity = await _context.Set<Entity>().Where(x=>x.Id==id).CountAsync();
            if (entity ==0) return false;
            return true;
        }


        public IEnumerable<Entity> FindWhere(Expression<Func<Entity, bool>> expression)
        {
            var result=_context.Set<Entity>().Where(expression).ToList();
            return result;
        }
        
        

        public IEnumerable<Entity>? GetAll()
        {
            var results = _context.Set<Entity>().ToList();
            return results;
        }

        public async Task<IEnumerable<Entity>?> GetAllAsync()
        {
            var result= await _context.Set<Entity>().ToListAsync();
            return result;
        }

        public  Entity? GetById(Guid id)
        {
            var result = _context.Set<Entity>().Find(id);
            return result;
        }

        public async Task<Entity?> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<Entity>().FindAsync(id);
            return result;
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
