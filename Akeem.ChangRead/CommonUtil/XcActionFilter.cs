using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public class XcActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        sb.Append(error.ErrorMessage + "|");
                    }
                }
                //Common.Error( ExceptionEnum.Parameter, sb.ToString());
                context.Result = new JsonResult(sb.ToString());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
