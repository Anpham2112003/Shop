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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository<Comment>
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetCommentByProductId(Guid id, int page, int take)
        {
            var Result= await _context.Set<Comment>()
                .Where(x=>x.ProductId==id)
                .Skip((page-1)*take)
                .Take(take)
                .ToListAsync();
            return Result;
        }
    }
}
