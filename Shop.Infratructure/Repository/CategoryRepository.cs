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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Category>().CountAsync();
        }

        public async Task<IEnumerable<Category>> GetProductsByCategoryId(Guid Id, int page, int take)
        {
           var Result= 

                await _context.Set<Category>()
                .Where(x=>x.Id==Id).AsNoTracking()
                .Include(x=>x.ProductCategories)!
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.Images)
                .Skip((page-1)*take).Take(take)
                .ToListAsync();  

            return Result;
        }
    }
}
