using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Order>().CountAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderIsPaymentByUserId(Guid Id, int page, int take)
        {
            var Result= await _context.Set<Order>()
                .Where(x=>x.Id==Id&&x.IsPayment==true)
                .AsNoTracking()
                .Skip((page-1)*take).Take(take)
                .ToListAsync();
            return Result;
        }

        public async Task<IEnumerable<Order>> GetOrderNoPaymentByUserId(Guid Id, int page, int take)
        {
            var Result = await _context.Set<Order>()
                .Where(x => x.Id == Id && x.IsPayment == false)
                .AsNoTracking()
                .Skip((page - 1) * take).Take(take)
                .ToListAsync();
            return Result;
        }
    }
}
