using Microsoft.AspNetCore.Mvc;
using Store.G02.Apis.MiddleWares;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence;
using Store.G02.Services;
using Store.G02.Shared.ErrorModles;


namespace Store.G02.Apis.ExtentionMethods
{
    public static class Extentions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Add services to the container.

            services.AddWebServices();
            services.AddInfrastractureServices(configuration);

            services.AddApplactionServices(configuration);

            services.AddApiBehaviorOptions();

            return services;
        }

        private static IServiceCollection AddApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                         .Select(M => new ValidationError()
                                                         {
                                                             Filed = M.Key,
                                                             Error = M.Value.Errors.Select(E => E.ErrorMessage)

                                                         }).ToList();

                    var response = new ValdationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };

            });

            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }










        public static async Task<WebApplication> ConfigureMiddleWaresAsync( this WebApplication app)
        {

            app.UseStaticFiles();

            app.UseGlobalErrorHandling();

            // Initialize the database Ask From CLR
            await app.SeedData();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            #region Using IServiceScope to create a scope To Initalize DB
            var scope = app.Services.CreateScope();
            var DBInitalizer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
            await DBInitalizer.InitalizeAsync();

            #endregion
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app) {

            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

            return app;
        }
    }

}



