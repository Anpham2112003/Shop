using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.AplicatonDBcontext;

namespace Shop.Infratructure.Repository;

public class TagRepository:GenericRepository<Tag>,ITagRepository<Tag>
{
    private readonly ApplicationDbContext _context;
    public TagRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddTagToProduct(Guid TagId, Guid ProductId)
    {
       await _context.Set<ProductTag>().AddAsync(new ProductTag { ProductId = ProductId ,TagId=TagId});

       
    }

    public  void RemoveTagProduct(ProductTag productTag)
    {
        _context.Set<ProductTag>().Remove(productTag);

    }

    public async Task<ProductTag?> FindProductTag(Guid TagId, Guid ProductId)
    {
        return await _context.Set<ProductTag>().Where(x => x.TagId == TagId && x.ProductId == ProductId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProductPreviewResponseModel>?> GetProductByTagId(Guid id, int page,int take)
    {
        var result = await _context.Set<Tag>()
            .Where(x=>x.Id==id)
            .Include(x=>x.Products)
            .ThenInclude(x=>x.Image)
            .Select(x=>x.Products.Select(x=>new ProductPreviewResponseModel {

                Id = x.Id,
                Image=x.Image,
                Name=x.Name,
                Price=x.Price,
                IsDiscount=x.IsDiscount,
                PriceDiscount=x.PriceDiscount
            }).Skip((page-1)*take).Take(take)).FirstOrDefaultAsync();

        return result;
    }

    public async Task<int> CountProductByTagId(Guid id)
    {
        var result = await _context.Set<ProductTag>().Where(x => x.TagId == id).CountAsync();

        return result ;
    }
}