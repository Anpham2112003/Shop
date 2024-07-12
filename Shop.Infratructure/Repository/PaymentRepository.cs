using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Payment>> GetPaymentByUserId(Guid id)
    {
        var payments = await _context.Set<Payment>()
                .Where(x => x.UserId == id).ToListAsync();

        return payments;
    }

    public async Task<int> CountPaymentByUserId(Guid id)
    {
        return await _context.Set<Payment>().Where(x=>x.UserId == id).CountAsync();
    }
}