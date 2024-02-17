using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.Repository;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infratructure.Dependency
{
    public static class DependencyInject
    {
        public static IServiceCollection AddInfratrcuture(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("SQL"), op =>
                {
                    op.MigrationsAssembly("Shop.Infratructure");
                });
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IUserRepository<>), typeof(UserRepository));
            services.AddSingleton<IUnitOfWork, UnitofWork>();
            return services;
        }
    }
}
