using Shop.Domain.Entities;
using Shop.Infratructure;
using Shop.Infratructure.Asbtraction;
using Shop.Infratructure.Repositories;

namespace Shop.Api.Extentions
{
    public static class Extention
    {
        public static void addSevices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<UserRepository>();
        }
    }
}
