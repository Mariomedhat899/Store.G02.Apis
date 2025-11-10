
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Store.G02.Apis.ExtentionMethods;
using Store.G02.Apis.MiddleWares;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Services;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Mapping.Products;
using Store.G02.Shared.ErrorModles;
using System.Threading.Tasks;

namespace Store.G02.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastractureServices(builder.Configuration);
            
            builder.Services.AddApplactionServices(builder.Configuration);

            builder.Services.AddAllServices(builder.Configuration);
           
            

            var app = builder.Build();

           await app.ConfigureMiddleWaresAsync();

            app.Run();
        }
    }
}
