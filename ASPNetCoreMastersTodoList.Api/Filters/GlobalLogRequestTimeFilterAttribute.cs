using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Api.Filters
{
    public class GlobalLogRequestTimeFilterAttribute : Attribute, IActionFilter
    {
        readonly Stopwatch _stopwatch = new Stopwatch();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            Console.WriteLine(context.HttpContext.Request.Path + " - Elapse Time: " + _stopwatch.ElapsedMilliseconds);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }
    }
}
