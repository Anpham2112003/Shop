using Microsoft.EntityFrameworkCore.Storage;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> userRepository { get; }
        IProductRepository<Product> productRepository { get; }
        ICommentRepository<Comment> commentRepository { get; }
        ICategoryRepository<Category> categoryRepository { get; }
        ICartRepository<Cart> cartRepository { get; }
        IImageRepository<Image> imageRepository { get; }
       
        IOrderRepository<Order> orderRepository { get; }
        IBrandRepository<Brand> brandRepository { get; }
        
        IRoleRepository<Role> roleRepository { get; }
        
        IShipRepository<Ship> shipRepository { get; }
        IAddressRepository<Address> addressRepository { get; }
        IPaymentRepository<Payment> paymentRepository { get; }
        
        ITagRepository<Tag> tagRepository { get; }
        public Task<int> SaveChangesAsync();
        public  Task<IDbContextTransaction> StartTransation();
    }
}
