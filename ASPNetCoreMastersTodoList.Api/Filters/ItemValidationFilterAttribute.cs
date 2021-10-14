using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Api.Filters
{
    public class ItemValidationFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int? itemId = 0;
            if (context.HttpContext.Request.Method.Equals("PUT"))
            {
                var itemDto = context.ActionArguments["item"] as ItemDto;
                itemId = itemDto.Id;
            }
            else
            { 
                itemId = context.ActionArguments["itemId"] as int?; 
            }
            var service = context.HttpContext.RequestServices.GetService(typeof(IItemService)) as IItemService;
            if (!service.GetAll().Any(x => x.Id.Equals(itemId)))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            
        }
    }
}
