using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentaion.Attr
{
    public class CacheAttibute(int TimeInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

           var CacheKey = GetCahceKey(context.HttpContext.Request);

            var result = await CacheService.GetDataAsync(CacheKey);
            if (!string.IsNullOrEmpty(result))
            {
                var Response = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                    
                };
                context.Result = Response;

                return;
            }

            var ActionContext = await next.Invoke();

            if(ActionContext.Result is OkObjectResult okObjectResult)
            {
               await CacheService.SetDataAsync(CacheKey, okObjectResult, TimeSpan.FromSeconds(TimeInSec));
            }
        }

        private string GetCahceKey(HttpRequest request)
        {
            var key = new StringBuilder();

            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");

            }

            return key.ToString();
        }
    }
}
