using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public class MyExceptionFilter : IExceptionFilter, IAsyncExceptionFilter, IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var result = context.Exception.ToError();
                context.Result = new ContentResult
                {
                    Content = result.ToJson(),
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "text/html;charset=utf-8"
                };
            }
            context.ExceptionHandled = true; //异常已处理了
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
