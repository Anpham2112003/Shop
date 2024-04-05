using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class PaymentRepository:GenericRepository<Payment>,IPaymentRepository<Payment>
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}