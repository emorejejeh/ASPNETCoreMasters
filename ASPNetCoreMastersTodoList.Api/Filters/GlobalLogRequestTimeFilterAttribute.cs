using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Api.Filters
{
    public class GlobalLogRequestTimeFilterAttribute : Attribute, IResourceFilter
    {
        readonly Stopwatch _stopwatch = new Stopwatch();

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _stopwatch.Stop();
            var elapseTime = context.HttpContext.Items["time"];
            Console.WriteLine(context.HttpContext.Request.Path + " - Elapse Time: " + elapseTime);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _stopwatch.Start();
            context.HttpContext.Items["time"] = _stopwatch.ElapsedMilliseconds;
        }
    }
}
