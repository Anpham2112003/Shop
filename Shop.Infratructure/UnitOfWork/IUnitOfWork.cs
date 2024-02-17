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
        IProductCategoryRepository<ProductCategory> productCategoryRepository { get; }
        IOrderRepository<Order> orderRepository { get; }
        IBrandRepository<Brand> brandRepository { get; }

        public void SaveChanges();
        public void SaveChangesAsync();

    }
}
