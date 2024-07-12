using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetOrderNoPaymentByUserId(Guid id, int page, int take)
        {
            var orders = await _context.Set<Order>()
                .Where(x => x.Id == id&&x.OrderState==Domain.Enums.OrderState.WaitPayment)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take).ToListAsync();

            return orders;
        }

       

        public async Task<List<OrderResponseModel>> GetOrderByUserId(Guid id, int page, int take)
        {
            var order = await _context.Set<Order>()
                .Where(x => x.UserId == id)
                .Include(x=>x.Product)
                .ThenInclude(x=>x.Image)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                
                .Select(x=>new OrderResponseModel()
                {
                    OrderId = x.Id,   
                    Amount = x.TotalPrice,                  
                    Quantity=x.Quantity,
                    OrderState=x.OrderState,
                    ImageUrl=x.Product!.Image!.ImageUrl,
                    ProductId=x.Product.Id,
                    ProductName=x.Product.Name,
                })
                .ToListAsync();

            return order;
        }

        public async Task<OrderDetailResponseModel?> GetOrderDetail(Guid id)
        {
            var order = await _context.Set<Order>()
                .Where(x => x.Id == id)
                .Include(x=>x.Ship)
                .Include(x=>x.Product)
                .ThenInclude(x=>x.Image)
                .AsNoTracking()
                .Select(x=>new OrderDetailResponseModel
                {
                    OrderId= x.Id,
                    ProductId=x.ProductId,
                    Quantity = x.Quantity,
                    Amount=x.TotalPrice,
                    OrderState = x.OrderState,
                    ImageUrl = x.Product!.Image!.ImageUrl,
                    ProductName = x.Product.Name,
                    Ship=x.Ship,
                    PaymentMethod=x.PaymentMethod,
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<int> CountOrderIdUser(Guid id)
        {
            return await _context.Set<Order>()
                .Where(x => x.Id == id)
                .CountAsync();
        }
        
        

      

       
    }
}
