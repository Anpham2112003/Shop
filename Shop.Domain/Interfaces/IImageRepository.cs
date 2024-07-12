using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
     public interface IImageRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
       
        
    }
    
}
