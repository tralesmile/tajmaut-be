using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace tajmautAPI.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomException customException)
            {
                context.Result = new ObjectResult(new { error = customException.Message })
                {
                    StatusCode = (int?)customException.StatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
