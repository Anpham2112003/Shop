using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class AddressRepository:GenericRepository<Address>,IAddressRepository<Address>
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Address>> GetAddressByUserId(Guid id)
    {
        var address = await _context.Set<Address>()
            .Where(x=>x.UserId == id)
            .AsNoTracking()
            .ToListAsync();
        
        return address;
    }

    
}