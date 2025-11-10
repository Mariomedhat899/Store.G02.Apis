using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence.Data.Contexts;
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

            return services;

        }
    }
}
