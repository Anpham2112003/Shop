using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IOrderRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        Task<IEnumerable<Entity>> GetOrderIsPaymentByUserId(Guid Id, int page, int take);
        Task<IEnumerable<Entity>> GetOrderNoPaymentByUserId(Guid Id, int page, int take);
        Task<int> CountAsync();
    }
}
