using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Repository
{
    class ImageRepository : GenericRepository<Image>, IImageRepository<Image>
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetImageByProductId(Guid id)
        {
            var Result= await _context.Set<Image>().Where(x=>x.ProductId==id).ToListAsync();
            return Result;
        }
    }
}
