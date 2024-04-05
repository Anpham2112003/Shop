using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;

using Shop.Aplication.Commands;
using Shop.Aplication.Commands.AddressCommand;
using Shop.Aplication.Commands.CartCommand;
using Shop.Aplication.Commands.CommentCommand;
using Shop.Aplication.Commands.OrderCommand;
using Shop.Aplication.Commands.ProductCommand;
using Shop.Aplication.Queries.ProductQueries;
using Shop.Aplication.Commands.ShipCommnd;

namespace Shop.Aplication.Mapper
{
    public class MapData:Profile
    {
       public MapData()
       {
          
           CreateMap<CreateUserCommand, User>()
               .ForMember(x => x.Id, op => op.MapFrom(x => x.Id))
               .ForMember(x => x.FistName, op => op.MapFrom(x => x.FistName))
               .ForMember(x => x.LastName, op => op.MapFrom(x => x.LastName))
               .ForMember(x => x.FullName, x => x.MapFrom(x => x.FullName))
               .ForMember(x => x.Email, x => x.MapFrom(x => x.Email))
               .ForMember(x => x.Password, x => x.MapFrom(x => x.Password))
               .ForMember(x => x.RoleId, x => x.MapFrom(x => x.RoleId))
               .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt))
               .ForMember(x=>x.IsActive,x=>x.MapFrom(x=>x.IsActive))
               ;
           
           
           
           

           CreateMap<User, UsersResponseModel>()
               .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
               .ForMember(x => x.FullName, x => x.MapFrom(x => x.FullName))
               .ForMember(x => x.Email, x => x.MapFrom(x => x.Email))
               .ForMember(x=>x.Role,m=>m.MapFrom(x=>x.Role.Name))
               .ForMember(x=>x.IsActive,m=>m.MapFrom(x=>x.IsActive))
               .ForMember(x=>x.IsDelete,m=>m.MapFrom(x=>x.IsDeleted))
               .ForMember(x=>x.DeletedAt,m=>m.MapFrom(x=>x.DeletedAt));
           
           

           CreateMap<UpdateUserCommand, User>()
               .ForMember(x => x.FistName, x => x.MapFrom(x => x.FistName))
               .ForMember(x => x.LastName, x => x.MapFrom(x => x.LastName))
               .ForMember(x => x.LastName, x => x.MapFrom(x => x.LastName))
               .ForMember(x => x.Password, x => x.MapFrom(x => x.Password))
               .ForMember(x => x.UpdatedAt, x => x.MapFrom(x => x.UpdatedAt));
           
           CreateMap<CreateCommentCommand, Comment>();
           

           

           CreateMap<CreateProductCommand, Product>()
               .ForMember(x=>x.Image,m=>m.Ignore())
               .ForMember(x => x.Id, x => x.MapFrom(x => x.Id))
               .ForMember(x => x.Name, x => x.MapFrom(x => x.Name))
               .ForMember(x => x.Price, x => x.MapFrom(x => x.Price))
               .ForMember(x => x.Quantity, x => x.MapFrom(x => x.Quantity))
               .ForMember(x => x.Description, x => x.MapFrom(x => x.Description))
               .ForMember(x => x.BrandId, x => x.MapFrom(x => x.BrandId))
               .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt))
               .ForMember(x=>x.CategoryId,m=>m.MapFrom(x=>x.CategoryId));

           CreateMap<UpdateProductCommand, Product>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.Name, m => m.MapFrom(x => x.Name))
               .ForMember(x => x.Quantity, m => m.MapFrom(x => x.Quantity))
               .ForMember(x => x.Price, m => m.MapFrom(x => x.Price))
               .ForMember(x => x.Description, m => m.MapFrom(x => x.Description))
               .ForMember(x => x.UpdatedAt, m => m.MapFrom(x => x.UpdatedAt));
           
           
           
           
           CreateMap<Product, ProductPreviewResponseModel>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.Name, m => m.MapFrom(x => x.Name))
               .ForMember(x => x.Image, m => m.MapFrom(x => x.Image))
               .ForMember(x => x.Price, m => m.MapFrom(x => x.Price));

           CreateMap<User, UserResponseModel>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.FistName, m => m.MapFrom(x => x.FistName))
               .ForMember(x => x.LastName, m => m.MapFrom(x => x.LastName))
               .ForMember(x => x.FullName, m => m.MapFrom(x => x.FullName));
           
           CreateMap<Product, GetProductByBrandIdResponseModel>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.Name, m => m.MapFrom(x => x.Name))
               .ForMember(x => x.Quantity, m => m.MapFrom(x => x.Quantity))
               .ForMember(x => x.Price, m => m.MapFrom(x => x.Price))
               .ForMember(x => x.Quantity, m => m.MapFrom(x => x.Image));
           
           
           CreateMap<CreateCartCommand, Cart>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.ProductName, m => m.MapFrom(x => x.ProductName))
               .ForMember(x => x.Quantity, m => m.MapFrom(x => x.Quantity))
               .ForMember(x => x.TotalPrice, m => m.MapFrom(x => x.TotalPrice))
               .ForMember(x => x.ImageUrl, m => m.MapFrom(x => x.ImageUrl))
               .ForMember(x => x.UserId, m => m.MapFrom(x => x.UserId))
               .ForMember(x => x.ProductId, m => m.MapFrom(x => x.ProductId));




           CreateMap<CreateOrderCommand, Order>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.Quantity, m => m.MapFrom(x => x.Quantity))
               .ForMember(x => x.TotalPrice, m => m.MapFrom(x => x.TotalPrice))
               .ForMember(x => x.ImageUrl, m => m.MapFrom(x => x.ImageUrl))
               .ForMember(x => x.UserId, m => m.MapFrom(x => x.UserId))
               .ForMember(x=>x.Name,m=>m.MapFrom(x=>x.Name))
               .ForMember(x=>x.PaymentMethod,m=>m.MapFrom(x=>x.Method))
               .ForMember(x => x.ProductId, m => m.MapFrom(x => x.ProductId));

           CreateMap<CreateAddressCommand, Address>()
               .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
               .ForMember(x => x.UserId, m => m.MapFrom(x => x.UserId))
               .ForMember(x => x.StreetAddress, m => m.MapFrom(x => x.StreetAddress))
               .ForMember(x => x.Commune, m => m.MapFrom(x => x.Commune))
               .ForMember(x => x.District, m => m.MapFrom(x => x.District))
               .ForMember(x => x.City, m => m.MapFrom(x => x.City));

           CreateMap<UpdateAddressCommand, Address>()
               .ForMember(x => x.StreetAddress, m => m.MapFrom(x => x.StreetAddress))
               .ForMember(x => x.Commune, m => m.MapFrom(x => x.Commune))
               .ForMember(x => x.District, m => m.MapFrom(x => x.District))
               .ForMember(x => x.City, m => m.MapFrom(x => x.City));


            CreateMap<CreateShipCommand, Ship>()
                .ForMember(x => x.Id, m => m.MapFrom(x => x.Id))
                .ForMember(x=>x.State,m=>m.MapFrom(x=>x.ShipState))
                .ForMember(x => x.OrderId, m => m.MapFrom(x => x.OrderId));
       }
       
    }
}
