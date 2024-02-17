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
        IGenericRepository<User> userRepository { get; }
        IGenericRepository<Product> productRepository { get; }
        IGenericRepository<Comment> commentRepository { get; }
        IGenericRepository<Category> categoryRepository { get; }
        IGenericRepository<Cart> cartRepository { get; }
        IGenericRepository<Image> imageRepository { get; }
        IGenericRepository<ProductCategory> productCategoryRepository { get; }
        IGenericRepository<Order> orderRepository { get; }
        IGenericRepository<Brand> brandRepository { get; }

        public void SaveChange();

    }
}
