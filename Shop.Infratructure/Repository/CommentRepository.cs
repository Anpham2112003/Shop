using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Domain.ResponseModel;
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

       

       

        public async Task<List<CommentResponseModel>> GetCommentByProductId(Guid id, int skip, int take)
        {
            var result= await _context.Set<Comment>()
                .Where(x=>x.ProductId==id)
                .Include(x=>x.User)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .Select(x=>new CommentResponseModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    ProductId = x.ProductId,
                    Rate=x.Rate,
                    UserName=x.User!.FullName,
                    Avatar=x.User!.AvatarUrl,
                })
                .ToListAsync();

            return result;
        }
    }
}
