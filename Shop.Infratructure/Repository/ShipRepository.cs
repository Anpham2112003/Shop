using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class ShipRepository:GenericRepository<Ship>,IShipRepository<Ship>
{
    private readonly ApplicationDbContext _context;

    public ShipRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}