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
                .Where(x => x.Id == id&&x.OrderState==false)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take).ToListAsync();

            return orders;
        }

       

        public async Task<List<OrderSuccessResponseModel>> GetOrderByUserId(Guid id, int page, int take)
        {
            var order = await _context.Set<Order>()
                .Where(x => x.UserId == id)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                
                .Select(x=>new OrderSuccessResponseModel()
                {
                    OrderId = id,
                    ProductName = x.Name,
                    Amount = x.TotalPrice,
                    ImageUrl = x.ImageUrl,
                    OrderState = x.OrderState
                })
                .ToListAsync();

            return order;
        }

        public async Task<Order?> GetOrderDetail(Guid id)
        {
            var order = await _context.Set<Order>()
                .Where(x => x.Id == id)
                .Select(x=>new Order()
                {
                    Id=x.Id,
                    Name=x.Name,
                    ImageUrl=x.ImageUrl,
                    OrderState = x.OrderState,
                    TotalPrice = x.TotalPrice,
                    ProductId = x.ProductId,
                    Ship  =  new Ship
                    {
                        Id = x.Ship!.Id,
                        Commune=x.Ship.Commune,
                        City=x.Ship.City,
                        StreetAddress=x.Ship.StreetAddress,
                        District=x.Ship.District,
                        State=x.Ship.State,
             
                    }
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<int> CountOrderIdUser(Guid id)
        {
            return await _context.Set<Order>()
                .Where(x => x.Id == id&&x.OrderState)
                .CountAsync();
        }
        
        

        public async Task<int> CountAsync()
        {
            return await _context.Set<Order>().CountAsync();
        }

      

       
    }
}
