using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Domain.ResponseModel;
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



        public async Task<List<Brand>> GetAllBrand( int page, int take)
        {
            var brands = await _context.Set<Brand>().Skip((page - 1) * take).Take(take).ToListAsync();
            
            return brands;
        }

        public async Task<IEnumerable<ProductPreviewResponseModel>?> GetProductByBrandId(Guid id,int page,int take)
        {
            var products = await _context.Set<Brand>()
                                    .Where(x=>x.Id == id)
                                    .Include(x=>x.Products)
                                    !.ThenInclude(x=>x.Image)
                                    .Select(x=>x.Products!.Select(x=> new ProductPreviewResponseModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Image = x.Image,
                                        IsDiscount = x.IsDiscount,
                                        Price = x.Price,
                                        PriceDiscount=x.PriceDiscount,
                                    })).FirstOrDefaultAsync();

            return products;
        }

        public async Task<int> CountProductByBrandId(Guid id)
        {
            return await _context.Set<Product>().Where(x=>x.BrandId==id).CountAsync();
        }
    }
}
