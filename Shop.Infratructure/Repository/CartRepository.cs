﻿using Microsoft.EntityFrameworkCore;
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
    public class CartRepository : GenericRepository<Cart>, ICartRepository<Cart>
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountCartByUserId(Guid id)
        {
            return await _context.Set<Cart>().Where(x => x.UserId == id).AsNoTracking().CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Cart>().CountAsync();
        }

        public async Task<List<Cart>> GetCartByUserId(Guid id, int page, int skip)
        {
            var result= await _context.Set<Cart>()
                .Where(x=>x.UserId == id)
                .Skip((page-1)*skip)
                .Take(skip)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}
