﻿using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IUserRepository<Entity>:IGenericRepository<Entity> where Entity :class,BaseEntity
    {
        Task<List<User>?> GetAllAsyncNoTracking(int page, int take);
        Task<Entity?> GetUserByIdAndRole(Guid id);
        Task<Entity?> GetUserByEmailAndRole(string? email);
        Task<int> Count();

        Task<Entity?> GetInfoUserById(Guid id);

    }
}
