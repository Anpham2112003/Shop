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
    class ImageRepository : GenericRepository<Image>, IImageRepository<Image>
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
