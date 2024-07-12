using Shop.Domain.Abstraction;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces;

public interface IPaymentRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class,BaseEntity
{
    public Task<List<Payment>> GetPaymentByUserId(Guid id);
    public Task<int> CountPaymentByUserId(Guid id);
}