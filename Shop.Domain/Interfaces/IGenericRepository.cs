using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        Entity? GetById(Guid id);
        IEnumerable<Entity>? GetAll();
        IEnumerable<Entity> FindWhere(Expression<Func<Entity,bool>> expression);
        Task<Entity?> FindByIdAsync(Guid id);
        void Add(Entity entity);
        void AddRange(IEnumerable<Entity> entities);

        void Update(Entity entity);
        void UpdateRange(IEnumerable<Entity> entity);
        void Remove(Entity entity);
        void RemoveRange(IEnumerable<Entity> entities);

        Task<Entity?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entity>?> GetAllAsync();
        Task AddAsync(Entity entity);
        Task AddRangeAsync(IEnumerable<Entity> entities);

        Task<bool> Check(Guid id);
        
        

    }
}
