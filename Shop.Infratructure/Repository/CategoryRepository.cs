using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.ResponseModel;

namespace Shop.Infratructure.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddCategoryToProduct(Guid CategoryId, Guid ProductId)
        {
           await _context.Set<ProductCategory>().AddAsync(new ProductCategory { ProductId = ProductId, CategoryId = CategoryId });
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Category>().CountAsync();
        }

        public async Task<int> CountProductInCategory(Guid CategoryId)
        {
            var res= await _context.Set<ProductCategory>().Where(x => x.CategoryId == CategoryId).CountAsync();

            return res;
        }

        public async Task<IEnumerable<ProductPreviewResponseModel>?> GetProductInCategory(Guid CategoryId, int page, int take)
        {
            var products = await _context.Set<Category>()
                                .Where(x=>x.Id==CategoryId)
                                    .Include(x=>x.Products)
                                    !.ThenInclude(x=>x.Image)
                                    
                                    .Select(x => x.Products!.Select(x=> new ProductPreviewResponseModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Image = x.Image,
                                        Price = x.Price,
                                       PriceDiscount = x.PriceDiscount,
                                       IsDiscount=x.IsDiscount,
                                    }).Skip((page-1)*take).Take(take)).FirstOrDefaultAsync();
            return products;                     
        }

        public async Task<ProductCategory?> FindProductCategory(Guid CategoryId,Guid ProductId)
        {
            return await _context.Set<ProductCategory>().Where(x=>x.ProductId==ProductId&&x.CategoryId==CategoryId).FirstOrDefaultAsync();
        }

        public void RemoveProductCatwgory(ProductCategory productCategory)
        {
            _context.Set<ProductCategory>().Remove(productCategory);
        }
    }
}
