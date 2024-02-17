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
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository<ProductCategory>
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<ProductCategory>().CountAsync();
        }
    }
}
