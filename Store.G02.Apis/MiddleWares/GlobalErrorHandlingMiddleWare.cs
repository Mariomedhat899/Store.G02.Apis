using Store.G02.Domain.Exceptions;
using Store.G02.Shared.ErrorModles;

namespace Store.G02.Apis.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleWare(RequestDelegate Next)
        {
            _next = Next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);

                if(context.Response.StatusCode == 404) //Routing MiddleWare
                {
                    context.Response.ContentType = "applaction/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"EndPoint {context.Request.Path} Was Not Found !!"
                    };

                   await context.Response.WriteAsJsonAsync(response);
                }

            }catch(Exception ex)
            {
                // Logic


                // 1. Set Status Code Of Response
                context.Response.StatusCode = ex switch
                {
                    NotFoundExeption => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,


                };
                // 2. Set Content Type Of Response
                context.Response.ContentType = "application/json";

                // 3. Set  Body Of Response
                var response = new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                };

                // Return Response

              await  context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
