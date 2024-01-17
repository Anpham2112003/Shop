using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Asbtraction
{
    public interface IRepository<Entity> where Entity : BaseEntity
    {
        public Task<Entity> GetById(Guid Id);
        public Task<List<Entity>> GetAll();
        public Task<int> DeleteById(Guid Id);
        public Task<int> UpdateById(Guid Id);
    }
}
