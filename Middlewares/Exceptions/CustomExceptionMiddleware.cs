using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace tajmautAPI.Middlewares.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                int statusCode = StatusCodes.Status500InternalServerError;
                string errorMessage = "An unexpected error occured";

                if (ex is CustomError customEx)
                {
                    statusCode = customEx.StatusCode;
                    errorMessage = customEx.ErrorMessage;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = errorMessage }));

            }
        }
    }
}
