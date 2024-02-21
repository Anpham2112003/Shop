using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.UnitOfWork
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private IUserRepository<User> UserRepository;
      
        public IUserRepository<User> userRepository
        {
            get
            {
                if(UserRepository == null)
                {
                    UserRepository= new UserRepository(_context);
                }
                return UserRepository;
            }
        }

        private IProductRepository<Product> ProductRepository;
        public IProductRepository<Product> productRepository
        {
            get {

                if(ProductRepository == null)
                    {
                        ProductRepository= new ProductRepository(_context);
                    } 
                return ProductRepository;
            }
        }
        public ICommentRepository<Comment> CommentRepository;
        public ICommentRepository<Comment> commentRepository
        {
            get
            {
                if(CommentRepository == null)
                {
                    CommentRepository= new CommentRepository(_context);
                }
                return CommentRepository;
            }
        }
        private ICategoryRepository<Category> CategoryRepository;

        public ICategoryRepository<Category> categoryRepository
        {
            get
            {
                if(CategoryRepository == null)
                {
                    CategoryRepository= new CategoryRepository(_context);
                }
                return CategoryRepository;
            }
        }
        private ICartRepository<Cart> CartRepository;

        public ICartRepository<Cart> cartRepository
        {
            get
            {
                if(CartRepository == null)
                {
                    CartRepository= new CartRepository(_context);
                }
                return CartRepository;
            }
        }

        private IImageRepository<Image> ImageRepository;
        public IImageRepository<Image> imageRepository {
            get
            {
                if(ImageRepository == null)
                {
                    ImageRepository= new ImageRepository(_context);
                }
                return ImageRepository;
            }
        }

        private IProductCategoryRepository<ProductCategory> ProductCategoryRepository;
        public IProductCategoryRepository<ProductCategory> productCategoryRepository
        {
            get
            {
                if(ProductCategoryRepository == null)
                {
                    ProductCategoryRepository= new ProductCategoryRepository(_context);
                }
                return ProductCategoryRepository;
            }
        }
        private IOrderRepository<Order> OrderRepository;
        public IOrderRepository<Order> orderRepository
        {
            get
            {
                if(OrderRepository == null)
                {
                    OrderRepository= new OrderRepository(_context);
                }
                return OrderRepository;
            }
        }
        private IBrandRepository<Brand> BrandRepository;
        public IBrandRepository<Brand> brandRepository
        {
            get
            {
                if(BrandRepository == null)
                {
                    BrandRepository= new BrandRepository(_context);
                }
                return BrandRepository;
            }
        }

        public void Dispose()
        {
            
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
    }
