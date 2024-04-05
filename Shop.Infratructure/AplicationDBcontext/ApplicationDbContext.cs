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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Brand>?  Brands { get; set; }
        public DbSet<ProductTag>? ProductTags { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Payment> Payments { get; set; }
       public DbSet<Ship>? Ships { get; set; }
       public DbSet<Address>? Addresses { get; set; }

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

                op.HasIndex(k => k.Id);

                op.HasOne(x => x.Role)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x=>x.RoleId)
                    .IsRequired();
                

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

            modelBuilder.Entity<Role>(op =>
            {
                op.HasKey(k => k.Id);
                op.HasData(
                    new Role(Guid.Parse("943bf4c8-4feb-43aa-9e58-4d92485c0078"), "Admin"),
                    new Role(Guid.Parse("b025ad27-3ad6-4fc7-9c50-2ec0b46f1d3b"), "Manager"),
                    new Role(Guid.Parse("c1bb2db4-a327-431f-9d7b-5122d6e17c28"), "User")
                );
            });

            modelBuilder.Entity<Product>(op =>
            {
                op.HasKey(k => k.Id);

                op.HasIndex(x => x.Id);

                op.HasOne(o => o.Brand)
                    .WithMany(m => m.Products)
                    .HasForeignKey(k => k.BrandId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

               

                op.HasMany(m=>m.Carts)
                    .WithOne(o=>o.Product)
                    .HasForeignKey(k => k.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasOne(o=>o.Image)
                    .WithOne(o=>o.Product)
                    .HasForeignKey<Image>(x=>x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                op.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();


                op.HasMany(x => x.Comments)
                    .WithOne(o => o.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            });

            modelBuilder.Entity<Category>(op =>
            {
                op.HasKey(k => k.Id);
                
            });

            modelBuilder.Entity<Brand>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Image>(op =>
            {
                op.HasKey(k => k.Id);
                op.HasIndex(k => k.Id);
            });

           
            modelBuilder.Entity<Comment>(op =>
            {
                op.HasKey(k => k.Id);
                op.HasIndex(k => k.Id);
            });
            modelBuilder.Entity<Cart>(op =>
            {
                op.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Order>(op =>
            {
                op.HasKey(k => k.Id);

                op.HasIndex(k => k.Id);

                op.HasIndex(x => x.ProductId);

                op.HasOne(o => o.Product)
                .WithOne(o => o.Order)
                .HasForeignKey<Order>(x=>x.ProductId)
                .HasConstraintName("order-product")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
                
              
                    
            });

            modelBuilder.Entity<Tag>(x =>
            {
                x.HasKey(k => k.Id);

               
            });
            modelBuilder.Entity<ProductTag>(x =>
            {
                x.HasKey(k => k.Id);
                
                x.HasIndex(k => k.ProductId);
                x.HasIndex(k => k.TagId);
                x.HasOne(o => o.Product)
                    .WithMany(m => m.ProductTags)
                    .HasForeignKey(fk => fk.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
                
                x.HasOne(o => o.Tag)
                    
                    .WithMany(m => m.ProductTags)
                    .HasForeignKey(fk => fk.TagId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Address>(op =>
            {
                op.HasKey(x => x.Id);
                
                op.HasIndex(x => x.Id);

                 op.HasOne(o=>o.User)
                     .WithMany(x=>x.Addresses)
                     .HasForeignKey(x=>x.UserId)
                     .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
             
                  
            });

            modelBuilder.Entity<Ship>(op =>
            {
                op.HasKey(x => x.Id);
                
                op.HasIndex(x => x.Id);
                
               

                op.HasOne(o => o.Order)
                    .WithOne(o => o.Ship)
                    .HasForeignKey<Ship>(x => x.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<Payment>(op =>
            {
                op.HasKey(x => x.Id);

                op.HasIndex(x => x.Id);

                op.HasOne(o => o.User)
                    .WithMany(x => x.Payments)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                op.HasOne(o=>o.Order)
                .WithOne(o=>o.Payment)
                .HasForeignKey<Payment>(x=>x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });



        }
    }
}
