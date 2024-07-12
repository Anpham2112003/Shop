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
    public class CartRepository : GenericRepository<Cart>, ICartRepository<Cart>
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountCartByUserId(Guid id)
        {
            return await _context.Set<Cart>().Where(x => x.UserId == id).AsNoTracking().CountAsync();
        }


        public async Task<List<CartResponseModel>> GetCartByUserId(Guid id, int page, int skip)
        {
            var result= await _context.Set<Cart>()
                .Where(x=>x.UserId == id)
                .Include(x=>x.Product)
                .ThenInclude(x=>x.Image)
                .Skip((page-1)*skip)
                .Take(skip)
                .Select(x=>new CartResponseModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName=x.Product!.Name,
                    Quantity = x.Quantity,
                    Cost=x.Product.Price ,
                    Amount=x.Product.IsDiscount? x.Product.PriceDiscount * x.Quantity: x.Product.Price*x.Quantity,
                    IsDiscount=x.Product.IsDiscount,
                    PriceDiscount=x.Product.PriceDiscount*x.Quantity,
                    Image=x.Product.Image!.ImageUrl
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}
