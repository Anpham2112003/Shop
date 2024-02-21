using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.AplicatonDBcontext
{
    public class ApplicationDbContext:DbContext
    {
       
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        

        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<ProductCategory>? ProductCategories { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Brand>?  Brands { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(op =>
            {
                op.HasKey(k => k.Id);

                op.HasMany(m => m.Carts)
                    .WithOne(o => o.User)
                    .HasForeignKey(k => k.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasMany(m => m.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasMany(m => m.Comments)
                    .WithOne(o => o.User)
                    .HasForeignKey(k => k.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();


            });

            modelBuilder.Entity<Product>(op =>
            {
                op.HasKey(k => k.Id);

                op.HasOne(o => o.Brand)
                    .WithMany(m => m.Products)
                    .HasForeignKey(k => k.BrandId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasMany(m=>m.ProductCategories)
                    .WithOne(o=>o.Product)
                    .HasForeignKey(k => k.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasMany(m=>m.Carts)
                    .WithOne(o=>o.Product)
                    .HasForeignKey(k => k.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasMany(m=>m.Images)
                    .WithOne(o=>o.Product)
                    .HasForeignKey(k => k.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                
            });

            modelBuilder.Entity<Category>(op =>
            {
                op.HasKey(k => k.Id);

                op.HasMany(m => m.ProductCategories)
                    .WithOne(o => o.Category)
                    .HasForeignKey(k => k.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Brand>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Image>(op =>
            {
                op.HasKey(k => k.Id);
            });

            modelBuilder.Entity<ProductCategory>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Comment>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Cart>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Order>(op =>
            {
                op.HasKey(k => k.Id);
                op.HasOne(o => o.Product)
                    .WithOne(o => o.Order)
                    .HasForeignKey<Order>(k => k.ProductFK)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            

        }
    }
}
