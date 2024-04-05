using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class TagRepository:GenericRepository<Tag>
{
    protected TagRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}