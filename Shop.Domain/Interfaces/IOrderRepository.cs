using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;

namespace Shop.Domain.Interfaces
{
    public interface IOrderRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class,BaseEntity
    {
        Task<List<Order>> GetOrderNoPaymentByUserId(Guid id, int page, int take);

        Task<List<OrderSuccessResponseModel>> GetOrderByUserId(Guid id, int page, int take);

        Task<Order?> GetOrderDetail(Guid id);
        Task<int> CountOrderIdUser(Guid id);
        
    }
}
