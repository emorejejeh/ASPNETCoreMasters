using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        [HttpGet()]
        public  IEnumerable<string> Get(int id)
        {
            ItemService itemService = new ItemService();
            return itemService.GetAll(id);
        }
    }
}
