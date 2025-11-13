using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class InfrastractureServicesRegisteration
    {
        public static IServiceCollection AddInfrastractureServices(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDBInitializer, DBInitalizer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepo, BasketRepo>();
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"))
            );

            return services;

        }
    }
}
