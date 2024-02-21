using AutoMapper;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.RequestModel;

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
               .ForMember(x => x.Role, x => x.MapFrom(x => x.Role))
               .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt));
               
       }
    }
}
