using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class RoleRepository:GenericRepository<Role>,IRoleRepository<Role>
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}