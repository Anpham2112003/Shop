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
                .Select(x=> new ProductResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Brand = x.Brand,
                    Image = x.Image
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
                    Image = x.Image
                }).OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();

           

            return product;
        }

        public async Task<List<GetProductByBrandIdResponseModel>> GetProductByBrandId(Guid id, int page, int take)
        {
            var products = await _context.Set<Product>()
                .Where(x => x.BrandId == id)
                .Skip((page - 1) * take).Take(take)
                .Include(x => x.Image)
                .Select(x => new GetProductByBrandIdResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Image = x.Image
                })
                .ToListAsync();
            return products;
        }

        public async Task<List<ProductPreviewResponseModel>> GetProductByCategoryId(Guid id, int page, int take)
        {
            var result = await _context.Set<Product>()
                .Where(x => x.CategoryId == id)
                .Skip((page - 1) * take).Take(take)
                .Include(x => x.Image)
                .Select(x => new ProductPreviewResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<int> CountProductByCategoryId(Guid id)
        {
            return await _context.Set<Product>().Where(x => x.CategoryId == id).AsNoTracking().CountAsync();
        }


        public async Task<int> CountProductByBrandId(Guid id)
        {
            var result = await _context.Set<Product>().Where(x => x.BrandId == id).AsNoTracking().CountAsync();

            return result;
        }

        public async Task<int> Count()
        {
            return await _context.Set<Product>().CountAsync();  
        }

        public async Task<List<ProductResponseModel>?> GetAllAsync(int page, int take)
        {
            var result= await _context.Set<Product>()
                .Include(x=>x.Brand)
                .Include(x=>x.Image)
                .AsNoTracking()
                .Skip((page-1)*take)
                .Take(take)
                .Select(x=>new ProductResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Brand = x.Brand,
                    Image = x.Image
                })
                .OrderBy(x=>x.Name)
                .ToListAsync();
            return result;
        }

    }
}
