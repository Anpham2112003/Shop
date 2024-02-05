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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository<Brand>
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
           return await _context.Set<Brand>().CountAsync();
        }

        public async Task<IEnumerable<Brand>> GetProductByBrandId(Guid Id, int page, int take)
        {
            var Result= await _context.Set<Brand>()
                .Where(x=>x.Id==Id)
                .AsNoTracking()
                .Include(x=>x.Products)
                !.ThenInclude(x=>x.Images)
                .Skip((page-1)*take).Take(take)
                .ToListAsync();  

            return Result;
        }
    }
}
