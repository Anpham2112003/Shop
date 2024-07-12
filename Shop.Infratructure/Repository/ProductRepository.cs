using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Shop.Domain.ResponseModel;

namespace Shop.Infratructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository<Product>
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductResponseModel?> GetProductDetails(Guid id)
        {
            var result = await _context.Set<Product>()
                .Where(x => x.Id == id)
                .Include(x=>x.Brand)
                .Include(x => x.Image)
                .Include(x=>x.Categories)
                .Include(x=>x.Tags)
                .Select(x=> new ProductResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Brand = x.Brand,
                    Image = x.Image,
                    Categories = x.Categories,
                    Tags = x.Tags,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<ProductPreviewResponseModel>> GetProductPreview(int page, int take)
        {
            var product =await _context.Set<Product>()
                .Skip((page - 1) * take).Take(take)
                .Include(x=>x.Image)
                .Select(x => new ProductPreviewResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    
                }).OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();

           

            return product;
        }

      public async Task<IEnumerable<ProductPreviewResponseModel>> SearchProduct(string Name,int skip,int take )
      {
           return await _context.Set<Product>()
                .Where(x=>x.Name!.StartsWith(Name))
                .Skip(skip).Take(take).Select(x=> new ProductPreviewResponseModel
                {
                    Id=x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    IsDiscount = x.IsDiscount,
                    PriceDiscount=x.PriceDiscount,
                })
                .ToListAsync();

      }


       



        public async Task<int> Count()
        {
            return await _context.Set<Product>().CountAsync();  
        }

       

    }
}
