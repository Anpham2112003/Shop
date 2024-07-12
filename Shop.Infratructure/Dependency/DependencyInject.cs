using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.Repository;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shop.Domain.Options;
using Shop.Infratructure.Services;
using Shop.Infratructure.Services.Aws3Service;
using Shop.Infratructure.Services.PaymentService;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.Services.VnPaySevice;
using StackExchange.Redis;

namespace Shop.Infratructure.Dependency
{
    public static class DependencyInject
    {
        public static IServiceCollection AddInfratrcuture(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("SQL")!, op =>
                {
                    op.MigrationsAssembly("Shop.Infratructure");
                    
                });

                op.LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddSingleton<IMailerSeverive, MailerService>();

            services.AddSingleton<IRedisService, RedisService>();
            
            services.AddScoped<IUnitOfWork, UnitofWork>();
            services.AddTransient<IAwsSevice, AwsService>();
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(JwtOptions.Jwt))
                .ValidateDataAnnotations();
            
            
            services.AddStackExchangeRedisCache(op =>
            {
                op.Configuration = configuration.GetConnectionString("Redis");
            });

            var awsConfig = configuration.GetAWSOptions();
            awsConfig.Region=RegionEndpoint.APSoutheast2;
            awsConfig.Credentials =
                new BasicAWSCredentials(configuration["AWS:AccessId"], configuration["AWS:RefreshKey"]);

            services.AddDefaultAWSOptions(awsConfig);
            services.AddTransient<IVnPayService, VNPayService>();
            services.AddAWSService<IAmazonS3>();

            
            return services;
        }
    }
}
