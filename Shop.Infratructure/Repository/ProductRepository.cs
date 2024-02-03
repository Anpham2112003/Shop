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
    public class ProductRepository : GenericRepository<Product>, IProductRepository<Product>
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            return await _context.Set<Product>().CountAsync();  
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int page, int take)
        {
            var Result= await _context.Set<Product>()
                .Include(x=>x.Brand)
                .Include(x=>x.ProductCategories)
                !.ThenInclude(x=>x.Category)
                .AsNoTracking()
                .Skip((page-1)*take)
                .Take(take)
                .ToListAsync();
            return Result;
        }

    }
}
