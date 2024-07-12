using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;

namespace Shop.Domain.Interfaces
{
    public interface ICommentRepository<Entity> : IGenericRepository<Entity> where Entity : class ,BaseEntity
    {
        Task<List<CommentResponseModel>> GetCommentByProductId(Guid id, int skip, int take);
        
    }
   
}
