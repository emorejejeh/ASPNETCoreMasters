using ASPNetCoreMastersTodoList.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;
using System.Collections.Generic;

namespace ASPNetCoreMastersTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{itemId}")]
        public IActionResult Get(int itemId)
        {
            return Ok(_itemService.GetById(itemId));
        }

        [HttpGet("filter")]
        public IActionResult GetByFilters([FromQuery] Dictionary<string, string> filters)
        {
            return null;
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ItemCreateApiModel model)
        {
            _itemService.Save(new ItemDto { Item = model.Item });
            return Ok();
        }

        [HttpPut("{itemId}")]
        public IActionResult Put(int itemId)
        {
            _itemService.UpdateItem(itemId);
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public IActionResult Delete(int itemId)
        {
            _itemService.DeleteItem(itemId);
            return Ok();
        }
    }
}
