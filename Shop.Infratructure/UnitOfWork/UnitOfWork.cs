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
        private IGenericRepository<User> UserRepository;
      
        public IGenericRepository<User> userRepository
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

        private IGenericRepository<Product> ProductRepository;
        public IGenericRepository<Product> productRepository
        {
            get {
                if(ProductRepository == null)
                {
                    ProductRepository= new ProductRepository(_context);
                } 
                return ProductRepository;
            }
        }
        public IGenericRepository<Comment> CommentRepository;
        public IGenericRepository<Comment> commentRepository
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
        private IGenericRepository<Category> CategoryRepository;

        public IGenericRepository<Category> categoryRepository
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
        private IGenericRepository<Cart> CartRepository;

        public IGenericRepository<Cart> cartRepository
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

        private IGenericRepository<Image> ImageRepository;
        public IGenericRepository<Image> imageRepository {
            get
            {
                if(ImageRepository == null)
                {
                    ImageRepository= new ImageRepository(_context);
                }
                return ImageRepository;
            }
        }

        private IGenericRepository<ProductCategory> ProductCategoryRepository;
        public IGenericRepository<ProductCategory> productCategoryRepository
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
        private IGenericRepository<Order> OrderRepository;
        public IGenericRepository<Order> orderRepository
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
        private IGenericRepository<Brand> BrandRepository;
        public IGenericRepository<Brand> brandRepository
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

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
    }
