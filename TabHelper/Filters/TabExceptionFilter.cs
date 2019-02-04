using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TabHelper.Services;

namespace TabHelper.Filters
{
    public class TabExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if(isAjax)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = 500;
                var message = context.Exception is DomainValidation ? context.Exception.Message : "Um erro aconteceu.";
                context.Result = new JsonResult(message);
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
