using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ITagRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        public Task AddTagToProduct(Guid TagId, Guid ProductId);

        public void RemoveTagProduct(ProductTag productTag);
        public Task<ProductTag?> FindProductTag(Guid TagId, Guid ProductId);
        public  Task<IEnumerable<ProductPreviewResponseModel>?> GetProductByTagId(Guid id, int page, int take);
        public Task<int> CountProductByTagId(Guid id);
    }
}
